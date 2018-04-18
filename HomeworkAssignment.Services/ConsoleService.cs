using HomeworkAssignment.Core;
using HomeworkAssignment.Core.Properties;
using HomeworkAssignment.Domain.Enums;
using HomeworkAssignment.Domain.Models;
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
        private readonly IDataParserStrategy parserStrategy;
        private readonly IDataStorageService dataStorageService;
        private readonly ISortingStrategy sortingStrategy;

        public ConsoleService(IFileService fileService, ILogService logService, IDataParserStrategy parserStrategy, 
            IDataStorageService dataStorageService, ISortingStrategy sortingStrategy)
        {
            this.fileService = fileService;
            this.logService = logService;
            this.parserStrategy = parserStrategy;
            this.dataStorageService = dataStorageService;
            this.sortingStrategy = sortingStrategy;
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
                var records = await this.fileService.ReadAsync(input);
                if (records != null && records.Any())
                {
                    var parser = parserStrategy.GetDataParser(records.First());
                    var parsedRecords = parser.Parse(records, skipInvalid: true);
                    if (parsedRecords != null && parsedRecords.Any())
                    {
                        dataStorageService.Store(parsedRecords);
                    }
                }

                PrintInMemoryRecords();

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

            PrintMessage(string.Empty);
            PrintMessage(string.Format(Resources.Instructions, Constants.ExitCommand));

            return true;
        }

        private void HandleException(Exception ex)
        {
            PrintMessage(ex.Message);
            logService.LogException(ex);
        }

        private void PrintInMemoryRecords()
        {
            PrintMessage(Resources.PrintMessageNotice);
            var recordsList = dataStorageService.GetAll();
            if (recordsList != null && recordsList.Any())
            {
                SortAndPrint(SortStrategyEnum.GenderThenLastName, recordsList);
                SortAndPrint(SortStrategyEnum.BirthDate, recordsList);
                SortAndPrint(SortStrategyEnum.LastNameDesc, recordsList);
            }
        }

        private void SortAndPrint(SortStrategyEnum sortStrategyEnum, IEnumerable<RecordModel> recordsCollection)
        {
            PrintMessage(string.Empty);
            PrintMessage($"Sorting using {sortStrategyEnum}:");
            PrintRecordCollection(sortingStrategy.Sort(sortStrategyEnum, recordsCollection));
        }

        private void PrintRecordCollection(IEnumerable<RecordModel> recordsCollection)
        {
            foreach (var record in recordsCollection)
            {
                PrintMessage(record.ToString());
            }
        }
    }
}
