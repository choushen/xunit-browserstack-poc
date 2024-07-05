using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace XUnitBrowserStackPoC
{

    public class BrowserStackConfigManager
    {
        public string UserName { get; set; }
        public string AccessKey { get; set; }
        public string ProjectName { get; set; }
        public string BuildName { get; set; }
        public string BuildTag { get; set; }
        public string BuildIdentifier { get; set; }
        public string App { get; set; }
        public Platform[] Platforms { get; set; }
        public bool BrowserStackLocal { get; set; }
        public bool Debug { get; set; }
        public bool NetworkLogs { get; set; }
        public string ConsoleLogs { get; set; }

        public static BrowserStackConfigManager Load(string path)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            using (var reader = new StreamReader(path))
            {
                return deserializer.Deserialize<BrowserStackConfigManager>(reader);
            }
        }
    }

    public class Platform
    {
        public string DeviceName { get; set; }
        public string OsVersion { get; set; }
        public string PlatformName { get; set; }
    }
}

