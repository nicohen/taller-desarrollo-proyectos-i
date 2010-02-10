namespace ZoosManagementSystem.Core
{
    using System.ServiceProcess;
    using ZoosManagmentSystem.Core.Switch;

    public partial class ZoosManagementSystemService : ServiceBase
    {
        public ZoosManagementSystemService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            MockSensorManager mockSensorManager = new MockSensorManager();

            mockSensorManager.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
