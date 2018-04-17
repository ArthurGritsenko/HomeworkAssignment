using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment
{
    public class InputProcessor
    {
        public static async Task<bool> ProcessInput(string input, string exitCommand)
        {
            if (input == exitCommand)
            {
                return false;
            }

            return true;
        }
    }
}
