using HomeworkAssignment.Domain.Enums;
using HomeworkAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Interfaces
{
    public interface ISortingStrategy
    {
        IEnumerable<RecordModel> Sort(SortStrategyEnum sortStrategy, IEnumerable<RecordModel> dataToBeSorted);
    }
}
