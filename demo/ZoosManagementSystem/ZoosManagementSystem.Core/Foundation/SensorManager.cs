using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;
using ZoosManagementSystem.Core.Storage;

namespace ZoosManagmentSystem.Core.Foundation
{
    public abstract class SensorManager
    {
        #region Fields

        private string name;
        private Guid environmentId;
        private int actionExecutionDelay;
        protected List<TimeSlot> timeSlotEntries;
        private DbHelper dbHelper;

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

        public SensorManager(string name, int actionExecutionDelay, Guid environmentId)
        {
            this.name = name;
            this.actionExecutionDelay = actionExecutionDelay;
            this.timeSlotEntries = new List<TimeSlot>();
            this.environmentId = environmentId;
        }

        public void LoadDataFromStorage()
        {
            try
            {
                this.dbHelper = new DbHelper();
                this.timeSlotEntries = this.dbHelper.GetEnvironmentTimeSlots(this.environmentId);
            }
            catch (Exception e)
            {

            }
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
