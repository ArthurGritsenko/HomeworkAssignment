using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment.Interfaces
{
    public interface IFileService
    {
        Task<string[]> ReadAsync(string fileName);
    }
}
