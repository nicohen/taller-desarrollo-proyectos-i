using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;

namespace ZoosManagmentSystem.Core.Foundation
{
    public abstract class SensorManager
    {
        #region Fields

        private string name;
        private int actionExecutionDelay;
        //private List<ActionEntry> actionExecutionEntries;

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public int ActionExecutionDelay
        {
            get
            {
                return this.actionExecutionDelay;
            }
        }

        #endregion

        #region Methods

        public SensorManager(string name, int actionExecutionDelay)
        {
            this.name = name;
            this.actionExecutionDelay = actionExecutionDelay;
            //this.actionExecutionEntries = new List<ActionEntry>();
        }

        public virtual void Initialize(object settings)
        {

        }

        /// <summary>
        /// Starts monitoring and data retrieval
        /// </summary>
        public virtual void Start()
        {

        }

        /// <summary>
        /// Processes retrieved data which may result in executing an <see cref="IAction"/>.
        /// </summary>
        protected virtual void ProcessData()
        {

        }

        protected virtual void Stop()
        {

        }

        //protected virtual void ExecuteAction(IAction action)
        //{

        //}

        #endregion

    }
}
