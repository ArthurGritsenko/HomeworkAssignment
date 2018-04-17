using HomeworkAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Interfaces
{
    public interface IDataParser
    {
        IEnumerable<RecordModel> Parse(string[] data, bool skipInvalid);
    }
}
