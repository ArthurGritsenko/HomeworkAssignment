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
        /// <param name="records">data to parse</param>
        /// <param name="skipInvalid">true to skip invalid lines, false to throw an exception</param>
        /// <returns></returns>
        IEnumerable<RecordModel> Parse(string[] records, bool skipInvalid);

        /// <summary>
        /// Takes string of data and determines if the format is acceptable by current parser
        /// </summary>
        /// <param name="record">string to analyze</param>
        /// <returns>true if current parser is able to parse the string, false otherwise</returns>
        bool IsValid(string record);
    }
}
