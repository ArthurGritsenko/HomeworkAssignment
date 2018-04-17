using HomeworkAssignment.Core.Properties;
using HomeworkAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Services
{
    public class FileService : IFileService
    {
        private readonly ILogService logService;

        public FileService(ILogService logService)
        {
            this.logService = logService;
        }

        public async Task<string[]> ReadAsync(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(string.Format(ErrorResources.FileNotFoundError, fileName));
            }

            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var reader = new StreamReader(fs, Encoding.UTF8))
            {
                var result = await reader.ReadToEndAsync();
                logService.Log(result);

                return result.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
