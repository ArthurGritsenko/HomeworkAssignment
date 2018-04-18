using System;
using System.Collections.Generic;
using System.Linq;
using HomeworkAssignment.Domain.Enums;
using HomeworkAssignment.Domain.Models;
using HomeworkAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeworkAssignmentTests
{
    [TestClass]
    public class SortersTests
    {
        [TestMethod]
        public void Sorter_Test()
        {
            var sortStrategy = new SortingStrategy();

            var birthDateTest = sortStrategy.Sort(SortStrategyEnum.BirthDate, dataToSort);
            var lastNameDescTest = sortStrategy.Sort(SortStrategyEnum.LastNameDesc, dataToSort);
            var firstNameTest = sortStrategy.Sort(SortStrategyEnum.FirstName, dataToSort);
            var genderThenLastName = sortStrategy.Sort(SortStrategyEnum.GenderThenLastName, dataToSort);
            var genderTest = sortStrategy.Sort(SortStrategyEnum.Gender, dataToSort);

            Assert.AreEqual(birthDateTest.First().DateOfBirth.ToShortDateString(), dataToSort.Last().DateOfBirth.ToShortDateString());
            Assert.AreEqual(lastNameDescTest.First().LastName, dataToSort.First().LastName);
            Assert.AreEqual(firstNameTest.First().FirstName, dataToSort.ElementAt(1).FirstName);
            Assert.AreEqual(genderThenLastName.First().FirstName, dataToSort.Last().FirstName);
            Assert.AreEqual(genderThenLastName.Last().FirstName, dataToSort.ElementAt(1).FirstName);
            Assert.AreEqual(genderTest.First().Gender, GenderEnum.Female);
            Assert.AreEqual(genderTest.Last().Gender, GenderEnum.Male);
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
