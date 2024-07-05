using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace XUnitBrowserStackPoC
{
    public class BrowserStackConfigManager
    {
        public string userName { get; set; }
        public string accessKey { get; set; }
        public string projectName { get; set; }
        public string buildName { get; set; }
        public string buildTag { get; set; }
        public string buildIdentifier { get; set; }
        public string app { get; set; }
        public Platform[] platforms { get; set; }
        public bool browserstackLocal { get; set; }
        public bool debug { get; set; }
        public bool networkLogs { get; set; }
        public string consoleLogs { get; set; }

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
        public string deviceName { get; set; }
        public string osVersion { get; set; }
        public string platformName { get; set; }
    }
}
