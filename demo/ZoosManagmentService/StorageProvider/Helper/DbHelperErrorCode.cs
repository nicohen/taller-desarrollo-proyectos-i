using System;
using System.Collections.Generic;
using System.Text;

namespace DbHelper
{
    public enum DbHelperErrorCode
    {
        Unknown = 0,
        UniqueConstraint = 1,
        NullNotAllowed = 2,
        ForeignKeyConstraint = 3
    }
}