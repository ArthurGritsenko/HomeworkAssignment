using HomeworkAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Services
{
    public class DebugLogService : ILogService
    {
        public void Log(string str)
        {
            Debug.WriteLine(str);
        }

        public void LogException(Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }

        public void LogException(string str, Exception ex)
        {
            Debug.WriteLine(str);
            Debug.WriteLine(ex.ToString());
        }
    }
}
