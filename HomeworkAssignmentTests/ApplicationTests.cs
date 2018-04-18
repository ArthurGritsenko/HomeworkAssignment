using System;
using System.IO;
using System.Threading.Tasks;
using HomeworkAssignment;
using HomeworkAssignment.Interfaces;
using HomeworkAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HomeworkAssignmentTests
{
    [TestClass]
    public class ApplicationTests
    {
        private IConsoleService consoleService;
        private Mock<ILogService> logServiceMoq;
        private Mock<IFileService> fileServiceMoq;
        private Mock<IDataParserStrategy> parserStrategy;
        private Mock<IDataStorageService> dataStorageService;


        [TestInitialize]
        public void Initialize()
        {
            logServiceMoq = new Moq.Mock<ILogService>();
            logServiceMoq.Setup(x => x.Log(It.IsAny<string>()));

            fileServiceMoq = new Mock<IFileService>();
            fileServiceMoq
                .Setup(x => x.ReadAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new string[] { }));

            parserStrategy = new Mock<IDataParserStrategy>();
            dataStorageService = new Mock<IDataStorageService>();

            consoleService = new ConsoleService(
                fileServiceMoq.Object,
                logServiceMoq.Object,
                parserStrategy.Object,
                dataStorageService.Object);
        }

        [TestMethod]
        public async Task App_Exits_By_Command_Test()
        {
            var exitCommand = "q";

            var result = await consoleService.ProcessInputAsync(exitCommand, exitCommand);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Unexpected_File_Read_Exception_Exits_App_Test()
        {
            var fileServiceMoqWithException = new Mock<IFileService>();
            fileServiceMoqWithException.Setup(x => x.ReadAsync(It.IsAny<string>())).Throws(new Exception());

            var consoleServiceTest = new ConsoleService(
                fileServiceMoqWithException.Object,
                logServiceMoq.Object,
                parserStrategy.Object,
                dataStorageService.Object);

            var exitCommand = "q";

            var result = await consoleServiceTest.ProcessInputAsync(string.Empty, exitCommand);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task File_Not_Found_Exception_Does_Not_Exits_App()
        {
            var fileServiceMoqWithException = new Mock<IFileService>();
            fileServiceMoqWithException.Setup(x => x.ReadAsync(It.IsAny<string>())).Throws(new FileNotFoundException());

            var exitCommand = "q";

            var result = await consoleService.ProcessInputAsync(string.Empty, exitCommand);

            Assert.IsTrue(result);
        }
    }
}
