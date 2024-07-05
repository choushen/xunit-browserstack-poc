using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium;
using TechTalk.SpecFlow;


namespace XUnitBrowserStackPoC
{

    public class Hooks : IDisposable
    {
        public static IOSDriver<IOSElement> driver { get; private set; }
        private BrowserStackConfigManager _config;


        [BeforeScenario]
        public void Setup()
        {
            _config = BrowserStackConfigManager.Load("browserstack.yml");

            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("browserstack.user", _config.UserName);
            appiumOptions.AddAdditionalCapability("browserstack.key", _config.AccessKey);
            appiumOptions.AddAdditionalCapability("app", _config.App);
            appiumOptions.AddAdditionalCapability("device", _config.Platforms[0].DeviceName);
            appiumOptions.AddAdditionalCapability("os_version", _config.Platforms[0].OsVersion);
            appiumOptions.AddAdditionalCapability("project", _config.ProjectName);
            appiumOptions.AddAdditionalCapability("build", _config.BuildName);
            appiumOptions.AddAdditionalCapability("name", "Sample Test");
            appiumOptions.AddAdditionalCapability("browserstack.local", _config.BrowserStackLocal);
            appiumOptions.AddAdditionalCapability("browserstack.debug", _config.Debug);
            appiumOptions.AddAdditionalCapability("browserstack.networkLogs", _config.NetworkLogs);
            appiumOptions.AddAdditionalCapability("browserstack.consoleLogs", _config.ConsoleLogs);

            driver = new IOSDriver<IOSElement>(new Uri("http://127.0.0.1:4723/wd/hub"), appiumOptions);
        }

        [AfterScenario]
        public void Dispose()
        {
            driver.Quit();
        }

    }
}
