using HomeworkAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Interfaces
{
    public interface IDataStorageService
    {
        /// <summary>
        /// Stores record in memory
        /// </summary>
        /// <param name="record"><see cref="RecordModel"/> to store</param>
        void Store(RecordModel record);

        /// <summary>
        /// Stores collection of records in memory
        /// </summary>
        /// <param name="records">Collection of <see cref="RecordModel"/></param>
        void Store(IEnumerable<RecordModel> records);

        /// <summary>
        /// Gets all the record that are currently stored in memory
        /// </summary>
        /// <returns>Collection of <see cref="RecordModel"/></returns>
        IEnumerable<RecordModel> GetAll();

        /// <summary>
        /// Removes all the objects from memory
        /// </summary>
        void Clear();
    }
}
