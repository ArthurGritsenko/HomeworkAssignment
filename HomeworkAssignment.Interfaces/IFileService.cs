using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Reads text from filename specified
        /// </summary>
        /// <param name="fileName">Path to the file to read text from</param>
        /// <returns>Array of strings</returns>
        Task<string[]> ReadAsync(string fileName);
    }
}
