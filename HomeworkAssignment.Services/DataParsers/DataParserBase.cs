using HomeworkAssignment.Core;
using HomeworkAssignment.Core.Properties;
using HomeworkAssignment.Domain.Enums;
using HomeworkAssignment.Domain.Models;
using HomeworkAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Services.DataParsers
{
    public abstract class DataParserBase : IDataParser
    {
        private readonly ILogService logService;

        public DataParserBase(ILogService logService)
        {
            this.logService = logService;
        }

        protected abstract string[] Delimeters { get; }

        public virtual IEnumerable<RecordModel> Parse(string[] records, bool skipInvalid)
        {
            var resultList = new List<RecordModel>();

            foreach (var record in records)
            {
                var splittedRecord = SplitRecord(record);

                try
                {
                    var model = ParseRecord(splittedRecord.ToArray());
                    resultList.Add(model);
                }
                catch (FormatException ex)
                {
                    logService.LogException(record, ex);
                    if (skipInvalid)
                    {
                        continue;
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    logService.LogException(record, ex);
                    throw;
                }
            }

            return resultList;
        }

        public virtual bool IsValid(string record)
        {
            try
            {
                // We can change this to accept the entire file and check if there is at least one valid string
                // But since requirements does not say anything about how should we handle invalid files 
                // let's assume that we can define validity by the first line only
                return ParseRecord(SplitRecord(record)) != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Splits a string into substring based on the strings in <see cref="Delimeters"/>
        /// </summary>
        /// <param name="record">line to split</param>
        /// <returns>splitted strings</returns>
        protected virtual string[] SplitRecord(string record)
        {
            if (string.IsNullOrWhiteSpace(record))
            {
                return new string[] { };
            }

            return record
                    .Split(Delimeters, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x?.Trim())
                    .ToArray();
        }

        /// <summary>
        /// Parses data provided and returns <see cref="RecordModel"/>
        /// </summary>
        /// <param name="modelData">Data to parse</param>
        /// <returns><see cref="RecordModel"/></returns>
        protected virtual RecordModel ParseRecord(string[] modelData)
        {
            if (modelData?.Count() != 5)
            {
                throw new FormatException(ErrorResources.RecordModelFieldsCountError);
            }

            if (!Enum.TryParse(modelData[2], out GenderEnum gender))
            {
                throw new FormatException(string.Format(ErrorResources.RecordModelInvalidGenderError, gender));
            }

            if (!DateTime.TryParseExact(modelData[4], Constants.RecordModelDateTimeFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
            {
                throw new FormatException(string.Format(ErrorResources.RecordModelInvalidDateError, Constants.RecordModelDateTimeFormat));
            }

            return new RecordModel()
            {
                LastName = modelData[0],
                FirstName = modelData[1],
                Gender = gender,
                FavoriteColor = modelData[3],
                DateOfBirth = dateOfBirth
            };
        }
    }
}
