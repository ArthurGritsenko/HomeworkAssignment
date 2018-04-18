using HomeworkAssignment.Interfaces;
using HomeworkAssignment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HomeworkAssignment.App_Start
{
    public static class ContainerConfig
    {
        /// <summary>
        /// Creates and initializes DI Container
        /// </summary>
        /// <returns>DI Container</returns>
        public static UnityContainer Configure()
        {
            var container = new UnityContainer();

            container.RegisterType<IConsoleService, ConsoleService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<ILogService, DebugLogService>();

            return container;
        }
    }
}
