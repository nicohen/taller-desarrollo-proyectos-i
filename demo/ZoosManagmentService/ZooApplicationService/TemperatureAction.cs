using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApplicationService
{
    public class TemperatureAction: IAction
    {
        #region IAction Members

        public bool Execute(params string[] executionParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
