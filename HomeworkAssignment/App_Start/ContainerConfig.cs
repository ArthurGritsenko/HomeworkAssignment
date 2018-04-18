using HomeworkAssignment.Interfaces;
using HomeworkAssignment.Services;
using HomeworkAssignment.Services.DataParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

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
            container.RegisterType<IDataStorageService, DataStorageService>(new ContainerControlledLifetimeManager());

            container.RegisterType<IDataParser, CommaDataParser>("comma");
            container.RegisterType<IDataParser, PipeDataParser>("pipe");
            container.RegisterType<IDataParser, SpaceDataParser>("space");

            container.RegisterType<IDataParserStrategy, DataParserStrategy>();

            return container;
        }
    }
}
