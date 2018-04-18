using HomeworkAssignment.Domain.Enums;
using HomeworkAssignment.Domain.Models;
using HomeworkAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Services
{
    public class SortingStrategy : ISortingStrategy
    {
        private Dictionary<SortStrategyEnum, Func<IEnumerable<RecordModel>, IEnumerable<RecordModel>>> StrategyMap
            = new Dictionary<SortStrategyEnum, Func<IEnumerable<RecordModel>, IEnumerable<RecordModel>>>()
            {
                { SortStrategyEnum.BirthDate, x => x.OrderBy(y => y.DateOfBirth ) },
                { SortStrategyEnum.LastNameDesc, x => x.OrderByDescending(y => y.LastName) },
                { SortStrategyEnum.FirstName, x => x.OrderBy(y => y.FirstName) },
                { SortStrategyEnum.GenderThenLastName, x=> x.OrderBy(y => y.Gender).ThenBy(y => y.LastName) },
                { SortStrategyEnum.Gender, x => x.OrderBy(y => y.Gender) }
            };

        public IEnumerable<RecordModel> Sort(SortStrategyEnum sortStrategy, IEnumerable<RecordModel> dataToBeSorted)
        {
            if (dataToBeSorted == null)
            {
                return Enumerable.Empty<RecordModel>();
            }

            if (!StrategyMap.ContainsKey(sortStrategy))
            {
                throw new ArgumentException($"Cannot find strategy for {sortStrategy}");
            }

            return StrategyMap[sortStrategy](dataToBeSorted);
        }
    }
}
