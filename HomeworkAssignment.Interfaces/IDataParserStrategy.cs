using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Interfaces
{
    public interface IDataParserStrategy
    {
        /// <summary>
        /// Creates parser that is able to parse data provided
        /// </summary>
        /// <param name="data">Example data string to return parser for</param>
        /// <returns>Specific implementation of <see cref="IDataParser"/></returns>
        IDataParser GetDataParser(string data);
    }
}
