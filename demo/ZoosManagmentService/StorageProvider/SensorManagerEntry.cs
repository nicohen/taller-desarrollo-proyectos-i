using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZooApplicationService.Storage.Provider
{
    public class SensorManagerEntry
    {
        #region Fields

        private string name;
        private string assemblyFullName;

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string AssemblyFullName
        {
            get
            {
                return this.assemblyFullName;
            }
            set
            {
                this.assemblyFullName = value;
            }
        }

        #endregion
    }
}
