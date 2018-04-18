using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Interfaces
{
    public interface ILogService
    {
        void Log(string str);
        void LogException(Exception ex);
        void LogException(string str, Exception ex);
    }
}
