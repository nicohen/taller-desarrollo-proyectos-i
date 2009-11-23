using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApplicationService
{
    public interface IAction
    {
        bool Execute(params string[] executionParameters);
    }
}
