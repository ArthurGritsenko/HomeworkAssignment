using HomeworkAssignment.Core;
using HomeworkAssignment.Core.Configurations;
using HomeworkAssignment.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkAssignment
{
    class Program
    {
        public static void Main(string[] args)
        {
            Init();

            Console.WriteLine(string.Format(Resources.Instructions, Constants.ExitCommand));

            while (InputProcessor.ProcessInput(Console.ReadLine(), Constants.ExitCommand).GetAwaiter().GetResult()) { };

            Console.WriteLine(Resources.ExitMessage);
        }

        private static void Init()
        {
            ContainerConfig.Configure();
        }
    }
}
