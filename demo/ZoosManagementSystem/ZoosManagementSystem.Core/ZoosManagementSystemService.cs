namespace ZoosManagementSystem.Core
{
    using System.ServiceProcess;

    public partial class ZoosManagementSystemService : ServiceBase
    {
        public ZoosManagementSystemService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
