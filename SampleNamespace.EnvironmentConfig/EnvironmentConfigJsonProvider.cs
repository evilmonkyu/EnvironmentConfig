using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SampleNamespace.EnvironmentConfig
{
    public class EnvironmentConfigJsonProvider : FileConfigurationProvider
    {
        private EnvironmentConfigJsonConfigurationSource source;

        public EnvironmentConfigJsonProvider(EnvironmentConfigJsonConfigurationSource configSource) : base(configSource)
        {
            source = configSource;
        }

        public override void Load(Stream stream)
        {
            var serializer = new JsonSerializer();
            var result = new Dictionary<string, string>();
            JArray config;

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                config = (JArray) serializer.Deserialize(jsonTextReader);
            }

            foreach (var token in config)
            {
                var entry = token as JObject;
                if (entry == null)
                    continue;

                JToken key;
                if (!entry.TryGetValue("name", out key) || key.Type != JTokenType.String)
                    continue;

                var environmentList = new List<string>()
                {
                    source.Environment != null ? $"{source.Environment}.local" : null,
                    "local",
                    source.Environment,
                    "default"
                };

                string value = null;
                foreach (var environment in environmentList)
                {
                    if (environment == null)
                        continue;
                    var prop = entry.Properties().SingleOrDefault(p => p.Name.Split(',').Contains(environment));
                    if (prop == null)
                        continue;
                    if (prop.Value.Type != JTokenType.String)
                        continue;
                    value = prop.Value.ToString();
                    break;
                }
                if (value == null)
                    continue;

                result.Add(key.ToString(), value);
            }

            Data = result;
        }
    }
}
