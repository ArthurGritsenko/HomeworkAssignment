using HomeworkAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Services
{
    public class ConsoleService : IConsoleService
    {
        private readonly IFileService fileService;
        private readonly ILogService logService;

        public ConsoleService(IFileService fileService, ILogService logService)
        {
            this.fileService = fileService;
            this.logService = logService;
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public async Task<bool> ProcessInputAsync(string input, string exitCommand)
        {
            if (input == exitCommand)
            {
                return false;
            }

            try
            {
                await this.fileService.ReadAsync(input);
            }
            catch (FileNotFoundException ex)
            {
                HandleException(ex);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }

            return true;
        }

        private void HandleException(Exception ex)
        {
            PrintMessage(ex.Message);
            logService.LogException(ex);
        }
    }
}
