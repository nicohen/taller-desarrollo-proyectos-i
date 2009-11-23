using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApplicationService
{
    public class ActionEntry
    {
        private IAction action;
        private float lowerLimit;
        private float upperLimit;
        private string[] executionParameters;
        private DateTime timeRangeStart;
        private DateTime timeRangeEnd;
    }
}
