using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleNamespace.EnvironmentConfig
{
    public class EnvironmentConfigJsonConfigurationSource : FileConfigurationSource
    {
        public string Environment { get; set; }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new EnvironmentConfigJsonProvider(this);
        }    
    }
}
