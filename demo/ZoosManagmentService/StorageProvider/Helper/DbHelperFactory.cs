using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DbHelper
{
    public static class DbHelperFactory
    {
        #region Methods

        public static DbHelper CreateDbHelper(string connectionName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionName];
            if (settings == null)
                throw new ArgumentException("Connection Name not found.");

            Type dbHelperType = Type.GetType(settings.ProviderName, true);
            if (!dbHelperType.IsSubclassOf(typeof(DbHelper)))
                throw new ArgumentException("Specified Connection Name does not reference a valid DbHelper type.");

            return (DbHelper)Activator.CreateInstance(dbHelperType, settings.ConnectionString);
        }
        
        public static string GetConnectionString(string connectionName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionName];
            if (settings == null)
                return null;

            return settings.ConnectionString;
        }

        #endregion
    }
}
