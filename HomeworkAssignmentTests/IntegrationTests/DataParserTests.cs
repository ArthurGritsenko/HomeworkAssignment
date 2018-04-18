using System;
using System.Linq;
using System.Threading.Tasks;
using HomeworkAssignment.Domain.Models;
using HomeworkAssignment.Interfaces;
using HomeworkAssignment.Services;
using HomeworkAssignment.Services.DataParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HomeworkAssignmentTests.IntegrationTests
{
    [TestClass]
    public class DataParserTests
    {
        private IDataParserStrategy dataParserStrategy;
        private IFileService fileService;

        private Mock<ILogService> logServiceMoq;

        [TestInitialize]
        public void Initialize()
        {
            logServiceMoq = new Mock<ILogService>();
            logServiceMoq.Setup(x => x.Log(It.IsAny<string>()));

            dataParserStrategy = new DataParserStrategy(new IDataParser[]
            {
                new CommaDataParser(logServiceMoq.Object),
                new PipeDataParser(logServiceMoq.Object),
                new SpaceDataParser(logServiceMoq.Object)
            });

            fileService = new FileService(logServiceMoq.Object);
        }

        [TestMethod]
        public async Task Parse_Comma_Separated_File_Test()
        {
            var filePath = @"IntegrationTests/SampleData/CommaDelimitedFile.txt";

            var data = await fileService.ReadAsync(filePath);
            var dataParser = dataParserStrategy.GetDataParser(data.FirstOrDefault());
            var models = dataParser.Parse(data, true);
            var resultModel = models.First();

            Assert.AreEqual(4, models.Count());
            Assert.AreEqual(TestRecordModel.LastName, resultModel.LastName);
            Assert.AreEqual(TestRecordModel.FirstName, resultModel.FirstName);
            Assert.AreEqual(TestRecordModel.Gender, resultModel.Gender);
            Assert.AreEqual(TestRecordModel.FavoriteColor, resultModel.FavoriteColor);
            Assert.AreEqual(TestRecordModel.DateOfBirth.ToShortDateString(), resultModel.DateOfBirth.ToShortDateString());
        }

        [TestMethod]
        public async Task Parse_Pipe_Separated_File_Test()
        {
            var filePath = @"IntegrationTests/SampleData/PipeDelimitedFile.txt";

            var data = await fileService.ReadAsync(filePath);
            var dataParser = dataParserStrategy.GetDataParser(data.FirstOrDefault());
            var models = dataParser.Parse(data, true);
            var resultModel = models.First();

            Assert.AreEqual(4, models.Count());
            Assert.AreEqual(TestRecordModel.LastName, resultModel.LastName);
            Assert.AreEqual(TestRecordModel.FirstName, resultModel.FirstName);
            Assert.AreEqual(TestRecordModel.Gender, resultModel.Gender);
            Assert.AreEqual(TestRecordModel.FavoriteColor, resultModel.FavoriteColor);
            Assert.AreEqual(TestRecordModel.DateOfBirth.ToShortDateString(), resultModel.DateOfBirth.ToShortDateString());
        }

        [TestMethod]
        public async Task Parse_Space_Separated_File_Test()
        {
            var filePath = @"IntegrationTests/SampleData/SpaceDelimitedFile.txt";

            var data = await fileService.ReadAsync(filePath);
            var dataParser = dataParserStrategy.GetDataParser(data.FirstOrDefault());
            var models = dataParser.Parse(data, true);
            var resultModel = models.First();

            Assert.AreEqual(4, models.Count());
            Assert.AreEqual(TestRecordModel.LastName, resultModel.LastName);
            Assert.AreEqual(TestRecordModel.FirstName, resultModel.FirstName);
            Assert.AreEqual(TestRecordModel.Gender, resultModel.Gender);
            Assert.AreEqual(TestRecordModel.FavoriteColor, resultModel.FavoriteColor);
            Assert.AreEqual(TestRecordModel.DateOfBirth.ToShortDateString(), resultModel.DateOfBirth.ToShortDateString());
        }

        #region TestData

        private RecordModel TestRecordModel
        {
            get
            {
                return new RecordModel()
                {
                    //Curtis, Alice, Male, Red, 1/12/2000
                    LastName = "Curtis",
                    FirstName = "Alice",
                    Gender = HomeworkAssignment.Domain.Enums.GenderEnum.Male,
                    FavoriteColor = "Red",
                    DateOfBirth = new DateTime(2000, 1, 12)
                };
            }
        }

        #endregion
    }
}
