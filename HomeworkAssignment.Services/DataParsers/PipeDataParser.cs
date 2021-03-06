﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeworkAssignment.Interfaces;

namespace HomeworkAssignment.Services.DataParsers
{
    public class PipeDataParser : DataParserBase
    {
        public PipeDataParser(ILogService logService) : base(logService)
        {
        }

        protected override string[] Delimeters
        {
            get
            {
                return new[] { "|" };
            }
        }
    }
}
