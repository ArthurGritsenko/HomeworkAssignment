using System;
using System.Linq;
using HomeworkAssignment.Domain.Models;
using HomeworkAssignment.Interfaces;
using HomeworkAssignment.Services;
using HomeworkAssignment.Services.DataParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HomeworkAssignmentTests
{
    [TestClass]
    public class DataParserTests
    {
        private SpaceDataParserTest spaceDataParser;
        private CommaDataParserTest commaDataParser;
        private Mock<ILogService> logServiceMoq;

        [TestInitialize]
        public void Initialize()
        {
            logServiceMoq = new Mock<ILogService>();
            logServiceMoq.Setup(x => x.Log(It.IsAny<string>()));

            spaceDataParser = new SpaceDataParserTest(logServiceMoq.Object);
            commaDataParser = new CommaDataParserTest(logServiceMoq.Object);
        }

        [TestMethod]
        public void IsValid_Returns_True_Test()
        {
            var record = RecordString;

            var result = spaceDataParser.IsValid(record);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_Returns_False_WrongDelimeter_Test()
        {
            var record = RecordStringWrongDelimeter;

            var result = spaceDataParser.IsValid(record);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Returns_False_WrongGender_Test()
        {
            var record = RecordStringWrongGender;

            var result = spaceDataParser.IsValid(record);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Returns_False_4Fields_Test()
        {
            var record = RecordString4Fields;

            var result = spaceDataParser.IsValid(record);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Returns_False_WrongDatetime_Test()
        {
            var record = RecordStringWrongDatetime;

            var result = spaceDataParser.IsValid(record);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Returns_False_If_Null_Or_Empty_Test()
        {
            var record1 = string.Empty;
            string record2 = null;

            var result1 = spaceDataParser.IsValid(record1);
            var result2 = spaceDataParser.IsValid(record2);

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void SplitRecord_Splits_On_Space_Test()
        {
            var _5fieldsString = RecordString;
            var _4fieldsString = RecordString4Fields;
            var commaDelimitedString = RecordStringWrongDelimeter;

            var _5fieldsResult = spaceDataParser.SplitRecordPublic(_5fieldsString);
            var _4fieldsResult = spaceDataParser.SplitRecordPublic(_4fieldsString);
            var commaDelimitedResult = commaDataParser.SplitRecordPublic(commaDelimitedString);

            Assert.AreEqual(5, _5fieldsResult.Count());
            Assert.AreEqual(4, _4fieldsResult.Count());
            Assert.AreEqual(5, commaDelimitedResult.Count());
        }

        [TestMethod]
        public void SplitRecord_With_Extra_Spaces_Test()
        {
            var commaRecordWithExtraSpaces = " Norman, Rosalie , Female  , Red   ,    12/1/1999 ";
            var spaceRecordWithExtraSpaces = " Norman   Rosalie   Female   Red   12/1/1999 ";

            var spaceResult = spaceDataParser.SplitRecordPublic(spaceRecordWithExtraSpaces);
            var commaResult = commaDataParser.SplitRecordPublic(commaRecordWithExtraSpaces);

            Assert.AreEqual(5, spaceResult.Count());
            Assert.AreEqual(5, commaResult.Count());
        }

        [TestMethod]
        public void SplitRecord_Returns_Empty_Array_If_Null_Or_Empty_Test()
        {
            var record1 = string.Empty;
            string record2 = null;

            var result1 = spaceDataParser.SplitRecordPublic(record1);
            var result2 = spaceDataParser.SplitRecordPublic(record2);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);

            Assert.AreEqual(0, result1.Count());
            Assert.AreEqual(0, result2.Count());
        }

        [TestMethod]
        public void ParseRecord_Returns_Model_Test()
        {
            var record = new string[] {
                "Norman",
                "Rosalie",
                "Female",
                "Red",
                "12/1/1999"
            };

            var resultModel = spaceDataParser.ParseRecordPublic(record);

            Assert.AreEqual(TestRecordModel.LastName, resultModel.LastName);
            Assert.AreEqual(TestRecordModel.FirstName, resultModel.FirstName);
            Assert.AreEqual(TestRecordModel.Gender, resultModel.Gender);
            Assert.AreEqual(TestRecordModel.FavoriteColor, resultModel.FavoriteColor);
            Assert.AreEqual(TestRecordModel.DateOfBirth.ToShortDateString(), resultModel.DateOfBirth.ToShortDateString());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = false)]
        public void ParseRecord_Throws_Exception_Invalid_Count()
        {
            var record = new string[] {
                "Norman",
                "Female",
                "Red",
                "12/1/1999"
            };

            var resultModel = spaceDataParser.ParseRecordPublic(record);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = false)]
        public void ParseRecord_Throws_Exception_Invalid_Gender()
        {
            var record = new string[] {
                "Norman",
                "Rosalie",
                "Femal",
                "Red",
                "12/1/1999"
            };

            var resultModel = spaceDataParser.ParseRecordPublic(record);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = false)]
        public void ParseRecord_Throws_Exception_Invalid_Date()
        {
            var record = new string[] {
                "Norman",
                "Rosalie",
                "Female",
                "Red",
                "1999/12/1"
            };

            var resultModel = spaceDataParser.ParseRecordPublic(record);

            Assert.Fail();
        }

        [TestMethod]
        public void ParseRecord_DateTime_Format_Test()
        {
            var record = new string[] {
                "Norman",
                "Rosalie",
                "Female",
                "Red"
            };

            var dt1 = record.Concat(new[] { "12/1/1999" }).ToArray();
            var dt2 = record.Concat(new[] { "12/31/1999" }).ToArray();
            var dt3 = record.Concat(new[] { "2/2/1999" }).ToArray();
            var dt4 = record.Concat(new[] { "02/02/1999" }).ToArray();

            var result1 = spaceDataParser.ParseRecordPublic(dt1);
            var result2 = spaceDataParser.ParseRecordPublic(dt2);
            var result3 = spaceDataParser.ParseRecordPublic(dt3);
            var result4 = spaceDataParser.ParseRecordPublic(dt4);

            Assert.AreEqual("12/1/1999", result1.DateOfBirth.ToShortDateString());
            Assert.AreEqual("12/31/1999", result2.DateOfBirth.ToShortDateString());
            Assert.AreEqual("2/2/1999", result3.DateOfBirth.ToShortDateString());
            Assert.AreEqual("2/2/1999", result4.DateOfBirth.ToShortDateString());
        }

        [TestMethod]
        public void ParseRecord_Invalid_Datetime_Test()
        {
            var record = new string[] {
                "Norman",
                "Rosalie",
                "Female",
                "Red"
            };

            var dt1 = record.Concat(new[] { "13/02/1999" }).ToArray();
            var dt2 = record.Concat(new[] { "2/30/1999" }).ToArray();
            var dt3 = record.Concat(new[] { "1999/02/02" }).ToArray();

            Assert.ThrowsException<FormatException>(() => spaceDataParser.ParseRecordPublic(dt1));
            Assert.ThrowsException<FormatException>(() => spaceDataParser.ParseRecordPublic(dt2));
            Assert.ThrowsException<FormatException>(() => spaceDataParser.ParseRecordPublic(dt3));
        }

        [TestMethod]
        public void Parse_Skips_Invalid_Test()
        {
            var records = new string[]
            {
                "Norman, Rosalie, Female, Red, 12/1/1999",
                "Norman Rosalie Female Red 12/1/1999",
            };

            var result = commaDataParser.Parse(records, skipInvalid: true);

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = false)]
        public void Parse_Throws_Exception_On_Invalid_Test()
        {
            var records = new string[]
            {
                "Norman, Rosalie, Female, Red, 12/1/1999",
                "Norman Rosalie Female Red 12/1/1999",
            };

            var result = commaDataParser.Parse(records, skipInvalid: false);

            Assert.Fail();
        }

        #region Test Data

        private const string RecordString = "Norman Rosalie Female Red 12/1/1999";
        private const string RecordString4Fields = "Rosalie Female Red 12/1/1999";
        private const string RecordStringWrongDelimeter = "Norman, Rosalie, Female, Red, 12/1/1999";
        private const string RecordStringWrongGender = "Norman Rosalie Test Red 12/1/1999";
        private const string RecordStringWrongDatetime = "Norman Rosalie Test Red 1999/12/1";

        private RecordModel TestRecordModel = new RecordModel()
        {
            LastName = "Norman",
            FirstName = "Rosalie",
            Gender = HomeworkAssignment.Domain.Enums.GenderEnum.Female,
            FavoriteColor = "Red",
            DateOfBirth = new DateTime(1999, 12, 1)
        };

        private class SpaceDataParserTest : SpaceDataParser
        {
            public SpaceDataParserTest(ILogService logService) : base(logService)
            {
            }

            public RecordModel ParseRecordPublic(string[] modelData)
            {
                return this.ParseRecord(modelData);
            }

            public string[] SplitRecordPublic(string record)
            {
                return this.SplitRecord(record);
            }
        }

        private class CommaDataParserTest : CommaDataParser
        {
            public CommaDataParserTest(ILogService logService) : base(logService)
            {
            }

            public string[] SplitRecordPublic(string record)
            {
                return this.SplitRecord(record);
            }
        }

        #endregion
    }
}
