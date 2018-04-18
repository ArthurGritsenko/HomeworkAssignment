﻿using HomeworkAssignment.Core.Properties;
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

        public ConsoleService(IFileService fileService, ILogService logService, IDataParserStrategy parserStrategy, IDataStorageService dataStorageService)
        {
            this.fileService = fileService;
            this.logService = logService;
            this.parserStrategy = parserStrategy;
            this.dataStorageService = dataStorageService;
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
                foreach (var record in recordsList)
                {
                    PrintMessage(record.ToString());
                }
            }
        }
    }
}
