using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SampleNamespace.EnvironmentConfig.Tests
{
    [TestClass]
    public class EnvironmentConfigJsonProviderTests
    {
        [TestMethod]
        public void ProvidesNullFromJsonFile()
        {
            var config = BuildConfig("sampleConfig.json");
            Assert.IsNull(config["SampleNoKey"]);
        }

        [TestMethod]
        public void ProvidesSampleValueFromJsonFile()
        {
            var config = BuildConfig("sampleConfig.json");
            Assert.AreEqual("SampleValue", config["SampleKey"]);
        }

        [TestMethod]
        public void ProvidesSampleValueFromJsonFileWithEnvironment()
        {
            var config = BuildConfig("sampleConfig.json", "test");
            Assert.AreEqual("SampleValue-test", config["SampleKey"]);
        }

        [TestMethod]
        public void ProvidesSampleValueFromJsonFileWithLocal()
        {
            var config = BuildConfig("sampleConfig.json", "test");
            Assert.AreEqual("SampleValue2-local", config["SampleKey2"]);
        }

        [TestMethod]
        public void ProvidesNullFromJsonFileIfNotString()
        {
            var config = BuildConfig("sampleConfig.json", "test");
            Assert.IsNull(config["SampleKey3"]);
        }

        [TestMethod]
        public void ProvidesSampleValueFromJsonFileWithEnvironmentMultiple()
        {
            var config = BuildConfig("sampleConfig.json", "test");
            Assert.AreEqual("SampleValue4-test", config["SampleKey4"]);
        }

        [TestMethod]
        public void ProvidesSampleValueFromJsonFileWithEnvironmentLocal()
        {
            var config = BuildConfig("sampleConfig.json", "test");
            Assert.AreEqual("SampleValue5-local", config["SampleKey5"]);
        }

        private IConfiguration BuildConfig(string file, string environment = null)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddEnvironmentConfigJson(file, environment).Build();
        }
    }
}
