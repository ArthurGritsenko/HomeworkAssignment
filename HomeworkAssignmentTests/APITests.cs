using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using HomeworkAssignment.Core;
using HomeworkAssignment.Domain.Enums;
using HomeworkAssignment.Domain.Models;
using HomeworkAssignment.Interfaces;
using HomeworkAssignment.Services;
using HomeworkAssignment.Services.DataParsers;
using HomeworkAssignment.WebAPI.Controllers;
using HomeworkAssignment.WebAPI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HomeworkAssignmentTests
{
    [TestClass]
    public class APITests
    {
        private Mock<IDataStorageService> dataStorageServiceMock;
        private Mock<ISortingStrategy> sortingStrategyMock;
        private Mock<IDataParserStrategy> dataParserStrategyMock;
        private Mock<ILogService> logServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            logServiceMock = new Mock<ILogService>();
            sortingStrategyMock = new Mock<ISortingStrategy>();

            dataStorageServiceMock = new Mock<IDataStorageService>();
            dataStorageServiceMock
                .Setup(x => x.GetAll())
                .Returns(() => dataToSort);

            dataParserStrategyMock = new Mock<IDataParserStrategy>();
            dataParserStrategyMock
                .Setup(x => x.GetDataParser(It.IsAny<string>()))
                .Returns(() => new CommaDataParser(logServiceMock.Object));
        }

        [TestMethod]
        public async Task GetSortedRecords_Should_Return_All_Records_In_Order_Test()
        {
            var controller = new RecordsController(
                dataStorageServiceMock.Object,
                dataParserStrategyMock.Object,
                new SortingStrategy());

            var sortedByGender = (await controller.GetSortedRecords("gender")) as OkNegotiatedContentResult<IEnumerable<RecordViewModel>>;
            var genderResults = sortedByGender.Content;

            var sortedByName = (await controller.GetSortedRecords("name")) as OkNegotiatedContentResult<IEnumerable<RecordViewModel>>;
            var nameResults = sortedByName.Content;

            var sortedByBirthdate = (await controller.GetSortedRecords("birthdate")) as OkNegotiatedContentResult<IEnumerable<RecordViewModel>>;
            var birthdateResults = sortedByBirthdate.Content;

            Assert.AreEqual(3, genderResults.Count());
            Assert.AreEqual(3, nameResults.Count());
            Assert.AreEqual(3, birthdateResults.Count());

            Assert.AreEqual("Female", genderResults.First().Gender);
            Assert.AreEqual("A", nameResults.First().FirstName);
            Assert.AreEqual(new DateTime().AddDays(1).ToString(Constants.RecordModelDateTimeFormat), birthdateResults.First().DateOfBirth);
        }

        [TestMethod]
        public async Task GetSortedRecords_Should_Return_Not_Found_Test()
        {
            var controller = new RecordsController(
                dataStorageServiceMock.Object,
                dataParserStrategyMock.Object,
                new SortingStrategy());

            var result = await controller.GetSortedRecords("test");

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task AddRecord_Should_Return_Created_Test()
        {
            var controller = new RecordsController(
               dataStorageServiceMock.Object,
               dataParserStrategyMock.Object,
               new SortingStrategy());
            var record = "Curtis, Alice, Male, Red, 1/12/2000";

            var result = (await controller.AddRecord(record)) as CreatedNegotiatedContentResult<RecordViewModel>;
            var resultModel = result.Content;

            Assert.AreEqual("Curtis", resultModel.LastName);
        }

        [TestMethod]
        public async Task AddRecord_Should_Return_BadRequest_Test()
        {
            var controller = new RecordsController(
               dataStorageServiceMock.Object,
               dataParserStrategyMock.Object,
               new SortingStrategy());
            var record = "Curtis, Alice, Male, Red| 1/12/2000";

            var result = (await controller.AddRecord(record));

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        #region TestData
        private IEnumerable<RecordModel> dataToSort = new List<RecordModel>()
        {
            new RecordModel()
            {
                FirstName = "B",
                LastName = "C",
                Gender = HomeworkAssignment.Domain.Enums.GenderEnum.Female,
                FavoriteColor = "",
                DateOfBirth = new DateTime().AddDays(3)
            },
            new RecordModel()
            {
                FirstName = "A",
                LastName = "B",
                Gender = HomeworkAssignment.Domain.Enums.GenderEnum.Male,
                FavoriteColor = "",
                DateOfBirth = new DateTime().AddDays(2)
            },
            new RecordModel()
            {
                FirstName = "C",
                LastName = "A",
                Gender = HomeworkAssignment.Domain.Enums.GenderEnum.Female,
                FavoriteColor = "",
                DateOfBirth = new DateTime().AddDays(1)
            }
        };

        #endregion
    }
}
