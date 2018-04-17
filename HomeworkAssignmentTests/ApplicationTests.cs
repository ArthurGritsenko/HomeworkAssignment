using System;
using System.Threading.Tasks;
using HomeworkAssignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeworkAssignmentTests
{
    [TestClass]
    public class ApplicationTests
    {
        [TestMethod]
        public async Task App_Exits_By_Command_Test()
        {
            var exitCommand = "q";

            var result = await InputProcessor.ProcessInput(exitCommand, exitCommand);

            Assert.AreEqual(false, result);
        }
    }
}
