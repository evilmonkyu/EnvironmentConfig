using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace SampleNamespace.EnvironmentConfig.Tests
{
    [TestClass]
    public class ReadEnvironmentVariablesTests
    {
        [TestMethod]
        public void CanReadEnvironmentVariablesTest()
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "../../../../SampleNamespace.EnvironmentConfig/readEnvironmentVariables.ps1 sampleConfig.json test",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            });
            process.WaitForExit();

            string error = process.StandardError.ReadToEnd();
            string result = process.StandardOutput.ReadToEnd() + error;            

            Assert.AreEqual("SampleKey=SampleValue-test\n" +
                "SampleKey2=SampleValue2-test\n" +
                "SampleKey3=3\n" +
                "SampleKey4=SampleValue4-test\n" +
                "SampleKey5=SampleValue5-test\n" +
                "SampleKey6=SampleValue6-test\n", result);       
        }

        [TestMethod]
        public void CanReadEnvironmentVariablesIntegration()
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "../../../../SampleNamespace.EnvironmentConfig/readEnvironmentVariables.ps1 sampleConfig.json integration",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            });
            process.WaitForExit();

            string error = process.StandardError.ReadToEnd();
            string result = process.StandardOutput.ReadToEnd() + error;

            Assert.AreEqual("SampleKey=SampleValue\n" +
                "SampleKey2=SampleValue2\n" +
                "SampleKey3=3\n" +
                "SampleKey4=SampleValue4-test\n" +
                "error SampleKey5\n" +
                "error SampleKey6\n", result);
        }
    }
}
