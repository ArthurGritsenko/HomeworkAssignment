using System;
using System.Linq;
using HomeworkAssignment.Interfaces;
using HomeworkAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeworkAssignmentTests
{
    [TestClass]
    public class DataStorageTests
    {
        [TestMethod]
        public void DataStorage_Store_Single_Test()
        {
            var dataStorage = new DataStorageService();
            var model = new HomeworkAssignment.Domain.Models.RecordModel();

            dataStorage.Store(model);
            var result = dataStorage.GetAll();

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void DataStorage_Store_Multiple_Test()
        {
            var dataStorage = new DataStorageService();
            var model = new HomeworkAssignment.Domain.Models.RecordModel[] 
            {
                new HomeworkAssignment.Domain.Models.RecordModel(),
                new HomeworkAssignment.Domain.Models.RecordModel()
            };

            dataStorage.Store(model);
            var result = dataStorage.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void DataStorage_Clean_Test()
        {
            var dataStorage = new DataStorageService();
            var model = new HomeworkAssignment.Domain.Models.RecordModel[]
            {
                new HomeworkAssignment.Domain.Models.RecordModel(),
                new HomeworkAssignment.Domain.Models.RecordModel()
            };

            dataStorage.Store(model);
            var result = dataStorage.GetAll();
            dataStorage.Clear();
            var result2 = dataStorage.GetAll();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(0, result2.Count());
        }
    }
}
