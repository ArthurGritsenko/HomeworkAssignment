using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HomeworkAssignment.Interfaces;
using HomeworkAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HomeworkAssignmentTests.IntegrationTests
{
    [TestClass]
    public class FileServiceTests
    {
        private Mock<ILogService> logServiceMoq;

        [TestInitialize]
        public void Initialize()
        {
            logServiceMoq = new Moq.Mock<ILogService>();
            logServiceMoq.Setup(x => x.Log(It.IsAny<string>()));
        }

        [TestMethod]
        public async Task Read_File_Test()
        {
            var service = new FileService(logServiceMoq.Object);
            var filePath = @"IntegrationTests/SampleData/CommaDelimitedFile.txt";

            var result = await service.ReadAsync(filePath);

            Assert.AreEqual(result.Count(), 4);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), AllowDerivedTypes = false)]
        public async Task Read_File_Throws_Not_Found_Exception_Test()
        {
            var service = new FileService(logServiceMoq.Object);
            var filePath = @"IntegrationTests/SampleData/DoesNotExists.txt";

            var result = await service.ReadAsync(filePath);

            Assert.Fail();
        }
    }
}
