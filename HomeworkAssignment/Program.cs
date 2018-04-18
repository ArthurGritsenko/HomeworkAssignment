using HomeworkAssignment.App_Start;
using HomeworkAssignment.Core;
using HomeworkAssignment.Core.Properties;
using HomeworkAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Attributes;

namespace HomeworkAssignment
{
    class Program
    {
        private static IConsoleService ConsoleService { get; set; }

        public static void Main(string[] args)
        {
            Init();

            ConsoleService.PrintMessage(string.Format(Resources.Instructions, Constants.ExitCommand));

            while (ConsoleService.ProcessInputAsync(ConsoleService.ReadLine(), Constants.ExitCommand).GetAwaiter().GetResult()) { };

            ConsoleService.PrintMessage(Resources.ExitMessage);
        }

        private static void Init()
        {
            var container = ContainerConfig.Configure();

            ConsoleService = container.Resolve<IConsoleService>();
        }
    }
}
