using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoosManagementSystem.Core.Foundation.Service
{
    public abstract class BaseService
    {
        protected bool running;
        protected int poolingInterval;

        public BaseService(int poolingInterval)
        {
            this.poolingInterval = poolingInterval;
        }

        public virtual void Initialize()
        {

        }

        protected abstract void OnStart();
        protected abstract void OnStop();

        public void Start()
        {
            this.running = true;
            this.OnStart();
        }

        public void Stop()
        {
            this.running = false;
            this.OnStop();
        }
    }
}
