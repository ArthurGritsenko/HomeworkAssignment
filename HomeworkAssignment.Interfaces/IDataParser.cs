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
        /// <summary>
        /// Parses the data
        /// </summary>
        /// <param name="data">data to parse</param>
        /// <param name="skipInvalid">true to skip invalid lines, false to throw an exception</param>
        /// <returns></returns>
        IEnumerable<RecordModel> Parse(string[] data, bool skipInvalid);
    }
}
