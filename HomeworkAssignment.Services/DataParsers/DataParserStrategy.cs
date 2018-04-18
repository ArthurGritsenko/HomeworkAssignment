using HomeworkAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Services.DataParsers
{
    public class DataParserStrategy : IDataParserStrategy
    {
        private readonly IDataParser[] dataParsers;

        public DataParserStrategy(IDataParser[] dataParsers)
        {
            this.dataParsers = dataParsers;
        }

        public IDataParser GetDataParser(string data)
        {
            var dataParser = dataParsers.FirstOrDefault(x => x.IsValid(data));
            if (dataParser == null)
            {
                throw new ArgumentException($"Could not find appropriate parser for {data}");
            }

            return dataParser;
        }
    }
}
