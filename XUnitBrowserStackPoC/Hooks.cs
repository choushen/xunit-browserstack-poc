using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium;
using TechTalk.SpecFlow;
using System;
using BrowserStack;
using System.IO;

namespace XUnitBrowserStackPoC
{
    [Binding]
    public class Hooks : IDisposable
    {
        public static IOSDriver<IOSElement> driver { get; private set; }
        private BrowserStackConfigManager _config;
        private Local _local;  // For BrowserStack Local

        [BeforeScenario]
        public void Setup()
        {
            // Get the root directory of the project
            var projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            var configFilePath = Path.Combine(projectDirectory, "browserstack.yml");

            _config = BrowserStackConfigManager.Load(configFilePath);

            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("browserstack.user", _config.userName);
            appiumOptions.AddAdditionalCapability("browserstack.key", _config.accessKey);
            appiumOptions.AddAdditionalCapability("app", _config.app);
            appiumOptions.AddAdditionalCapability("device", _config.platforms[0].deviceName);
            appiumOptions.AddAdditionalCapability("os_version", _config.platforms[0].osVersion);
            appiumOptions.AddAdditionalCapability("project", _config.projectName);
            appiumOptions.AddAdditionalCapability("build", _config.buildName);
            appiumOptions.AddAdditionalCapability("name", "Sample Test");
            appiumOptions.AddAdditionalCapability("browserstack.local", _config.browserstackLocal);
            appiumOptions.AddAdditionalCapability("browserstack.debug", _config.debug);
            appiumOptions.AddAdditionalCapability("browserstack.networkLogs", _config.networkLogs);
            appiumOptions.AddAdditionalCapability("browserstack.consoleLogs", _config.consoleLogs);

            if (_config.browserstackLocal)
            {
                _local = new Local();
                var localArgs = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("key", _config.accessKey) };
                _local.start(localArgs);
            }

            driver = new IOSDriver<IOSElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub"), appiumOptions);
        }

        [AfterScenario]
        public void Dispose()
        {
            driver.Quit();
            if (_local != null) _local.stop();
        }
    }
}
