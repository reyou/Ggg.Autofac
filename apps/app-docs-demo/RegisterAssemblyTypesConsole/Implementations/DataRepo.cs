using RegisterAssemblyTypesConsole.Interfaces;
using System;

namespace RegisterAssemblyTypesConsole.Implementations
{
    class DataRepo : IDataRepo
    {
        ILogger Logger { get; set; }
        public DataRepo(ILogger logger)
        {
            Logger = logger;
        }
        public int Create()
        {
            Console.WriteLine("Create is working!");
            Logger.Log();
            return 1;
        }

        public string Read()
        {
            Console.WriteLine("Read is working!");
            Logger.Log();
            return "read";
        }

        public bool Update()
        {
            Console.WriteLine("Update is working!");
            Logger.Log();
            return true;
        }
    }
}