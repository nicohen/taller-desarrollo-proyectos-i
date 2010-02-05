namespace ZoosManagementSystem.Core
{
    using System.ServiceProcess;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var ServicesToRun = new ServiceBase[] 
			{ 
				new ZoosManagementSystemService() 
			};

            ServiceBase.Run(ServicesToRun);
        }
    }
}
