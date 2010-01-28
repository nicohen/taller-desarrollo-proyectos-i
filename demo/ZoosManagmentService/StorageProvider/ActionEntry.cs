using System;
using System.Collections.Generic;
using System.Text;

using ZooApplicationService.Foundation.Api;

namespace ZooApplicationService.Storage.Provider
{
    public class ActionEntry
    {
        #region Fields

        private IAction action;
        private float lowerLimit;
        private float upperLimit;
        private string[] executionParameters;
        private DateTime timeRangeStart;
        private DateTime timeRangeEnd;

        #endregion

        #region Properties

        public IAction Action
        {
            get
            {
                return this.action;
            }
            set
            {
                this.action = value;
            }
        }

        public float LowerLimit
        {
            get
            {
                return this.lowerLimit;
            }
            set
            {
                this.lowerLimit = value;
            }
        }

        public float UpperLimit
        {
            get
            {
                return this.upperLimit;
            }
            set
            {
                this.upperLimit = value;
            }
        }

        public string[] ExecutionParameters
        {
            get
            {
                return this.executionParameters;
            }
            set
            {
                this.executionParameters = value;
            }
        }

        public DateTime TimeRangeStart
        {
            get
            {
                return this.timeRangeStart;
            }
            set
            {
                this.timeRangeStart = value;
            }
        }

        public DateTime TimeRangeEnd
        {
            get
            {
                return this.timeRangeEnd;
            }
            set
            {
                this.timeRangeEnd = value;
            }
        }

        #endregion

    }
}
