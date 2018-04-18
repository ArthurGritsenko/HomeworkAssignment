using HomeworkAssignment.Domain.Models;
using HomeworkAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Services
{
    public class DataStorageService : IDataStorageService
    {
        // According to requirements, we should not care about persistant storage, so use static variable instead
        private List<RecordModel> dataStorage = new List<RecordModel>();

        public void Clear()
        {
            dataStorage.Clear();
        }

        public IEnumerable<RecordModel> GetAll()
        {
            return dataStorage.ToList();
        }

        public void Store(RecordModel record)
        {
            dataStorage.Add(record);
        }

        public void Store(IEnumerable<RecordModel> records)
        {
            dataStorage.AddRange(records);
        }
    }
}
