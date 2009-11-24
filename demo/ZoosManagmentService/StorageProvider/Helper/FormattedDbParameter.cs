using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DbHelper
{
    public class FormattedDbParameter : ICloneable
    {
        private DbParameter dbParameter;
        private string originalName;

        public DbParameter DbParameter
        {
            get
            {
                return this.dbParameter;
            }
            set
            {
                this.dbParameter = value;
            }
        }

        public string OriginalName
        {
            get
            {
                return this.originalName; 
            }
            set 
            { 
                this.originalName = value; 
            }
        }

        internal FormattedDbParameter()
        {
        }

        public object Clone()
        {
            FormattedDbParameter clonedParameter = new FormattedDbParameter();
            clonedParameter.dbParameter = (DbParameter)((ICloneable)this.dbParameter).Clone();
            clonedParameter.originalName = this.originalName;

            return clonedParameter;
        }
    }
}
