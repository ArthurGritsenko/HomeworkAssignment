using HomeworkAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Interfaces
{
    public interface IConsoleService
    {
        /// <summary>
        /// Displays the message
        /// </summary>
        /// <param name="message">Message to display</param>
        void PrintMessage(string message);

        /// <summary>
        /// Reads line from the input source
        /// </summary>
        /// <returns></returns>
        string ReadLine();

        /// <summary>
        /// Gets input string and performs required actions over it
        /// </summary>
        /// <param name="input">The input string</param>
        /// <param name="exitCommand">string that will stop execution</param>
        /// <returns>false if input equals to exitCommand or if unexpected error happened, true otherwise</returns>
        Task<bool> ProcessInputAsync(string input, string exitCommand);
    }
}
