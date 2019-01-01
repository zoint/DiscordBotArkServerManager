using MadWorldStudios.DIscordBot.ASM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MadWorldStudios.DiscordBot.ASM.Tests
{
    [TestClass]
    public class ConfigurationManagerTests
    {
        [TestMethod]
        public void TestGetServerInfoFromAppSettings_ReturnsValidServer()
        {
            var configurationManager = new ConfigurationManager();

            var server = configurationManager.FindServerByName("Ohma");

            Assert.IsTrue(server.Name == "Ohma");
        }
    }
}
