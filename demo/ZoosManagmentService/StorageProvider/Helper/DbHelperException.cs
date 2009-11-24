using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DbHelper
{
    [Serializable]
    public class DbHelperException : Exception
    {
        #region Fields

        private DbHelperErrorCode helperErrorCode;

        #endregion

        #region Properties

        public DbHelperErrorCode HelperErrorCode
        {
            get
            {
                return this.helperErrorCode;
            }
        }

        #endregion

        #region Methods

        public DbHelperException(DbHelperErrorCode errCode)
            : base()
        {
            this.helperErrorCode = errCode;
        }

        public DbHelperException(DbHelperErrorCode errCode, string message)
            : base(message)
        {
            this.helperErrorCode = errCode;
        }

        public DbHelperException(DbHelperErrorCode errCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.helperErrorCode = errCode;
        }

        public DbHelperException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.helperErrorCode = (DbHelperErrorCode)info.GetValue("HelperErrorCode", typeof(DbHelperErrorCode));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("HelperErrorCode", this.helperErrorCode, typeof(DbHelperErrorCode));
        }
                
        #endregion
    }
}
