using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace ZooApplicationService.Storage.Helper
{
    public abstract class DbHelper
    {
        #region Fields

        private Hashtable commandTextCache = Hashtable.Synchronized(new Hashtable());
        private Hashtable commandParametersCache = Hashtable.Synchronized(new Hashtable());
        protected string connectionString;

        private enum DbConnectionOwnership
        {
            Internal,
            External
        }

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        #endregion

        #region Methods

        protected DbHelper(string connString)
        {
            this.connectionString = connString;
        }

        public FormattedDbParameter CreateParameter(string parameterName, object value)
        {
            FormattedDbParameter dbParameter = new FormattedDbParameter();
            dbParameter.OriginalName = parameterName;
            dbParameter.DbParameter = this.CreateProviderParameter(parameterName, value);

            return dbParameter;
        }

        public abstract DbConnection CreateConnection();
        
        public abstract DbParameter CreateProviderParameter(string parameterName, object value);
        
        protected abstract DbHelperException TranslateException(DbException ex);
        
        #endregion

        #region Static Helper Functions

        private static void PrepareCommand(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, FormattedDbParameter[] commandParameters)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            command.Connection = connection;
            command.CommandType = commandType;
            command.CommandText = commandText;
            
            if (transaction != null)
                command.Transaction = transaction;
            
            if (commandParameters != null)
            {
                foreach (FormattedDbParameter parameter in commandParameters)
                {
                    if ((parameter.DbParameter.Direction == ParameterDirection.InputOutput) && (parameter.DbParameter.Value == null))
                        parameter.DbParameter.Value = DBNull.Value;

                    command.Parameters.Add(parameter.DbParameter);
                }
            }
        }

        private static void AssignParameterValues(FormattedDbParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
                return;
            
            if (commandParameters.Length != parameterValues.Length)
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            
            for (int i = 0, numParams = commandParameters.Length; i < numParams; i++)
                commandParameters[i].DbParameter.Value = parameterValues[i];
        }

        protected static FormattedDbParameter[] CloneParameters(FormattedDbParameter[] originalParameters)
        {
            int numParameters = originalParameters.Length;

            FormattedDbParameter[] clonedParameters = new FormattedDbParameter[numParameters];
            for (int i = 0; i < numParameters; i++)
                clonedParameters[i] = (FormattedDbParameter)((ICloneable)originalParameters[i]).Clone();
            
            return clonedParameters;
        }
        
        #endregion
                
        #region ExecuteReader

        private DbDataReader ExecuteReader(DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, FormattedDbParameter[] commandParameters, DbConnectionOwnership connectionOwnership)
        {
            try
            {
                DbCommand command = connection.CreateCommand();
                DbHelper.PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);

                DbDataReader dataReader;
                if (connectionOwnership == DbHelper.DbConnectionOwnership.External)
                    dataReader = command.ExecuteReader();
                else
                    dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                command.Parameters.Clear();

                return dataReader;
            }
            catch (DbException ex)
            {
                throw this.TranslateException(ex);
            }
        }
        
        public DbDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return this.ExecuteReader(commandType, commandText, (FormattedDbParameter[])null);
        }
        
        public DbDataReader ExecuteReader(CommandType commandType, string commandText, params FormattedDbParameter[] commandParameters)
        {
            if (commandType == CommandType.Text)
            {
                if ((commandParameters != null) && (commandParameters.Length > 0))
                {
                    commandText = this.FormatCommandText(commandText, commandParameters);
                }
            }

            DbConnection connection = this.CreateConnection();
            try
            {
                connection.Open();
                return this.ExecuteReader(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal);
            }
            catch
            {
                connection.Close();
                throw;
            }
        }
        
        public DbDataReader ExecuteReader(string storedProcName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                FormattedDbParameter[] commandParameters = this.GetStoredProcParametersFromCache(storedProcName);
                DbHelper.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteReader(CommandType.StoredProcedure, storedProcName, commandParameters);
            }

            return this.ExecuteReader(CommandType.StoredProcedure, storedProcName);
        }
        
        public DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText)
        {
            return this.ExecuteReader(connection, commandType, commandText, (FormattedDbParameter[])null);
        }
        
        public DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText, params FormattedDbParameter[] commandParameters)
        {
            if (commandType == CommandType.Text)
            {
                if ((commandParameters != null) && (commandParameters.Length > 0))
                {
                    commandText = this.FormatCommandText(commandText, commandParameters);
                }
            }

            return this.ExecuteReader(connection, (DbTransaction)null, commandType, commandText, commandParameters, DbConnectionOwnership.External);
        }
        
        public DbDataReader ExecuteReader(DbConnection connection, string storedProcName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                FormattedDbParameter[] commandParameters = this.GetStoredProcParametersFromCache(storedProcName);
                DbHelper.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteReader(connection, CommandType.StoredProcedure, storedProcName, commandParameters);
            }

            return this.ExecuteReader(connection, CommandType.StoredProcedure, storedProcName);
        }
        
        public DbDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText)
        {
            return this.ExecuteReader(transaction, commandType, commandText, (FormattedDbParameter[])null);
        }
        
        public DbDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText, params FormattedDbParameter[] commandParameters)
        {
            if (commandType == CommandType.Text)
            {
                if ((commandParameters != null) && (commandParameters.Length > 0))
                {
                    commandText = this.FormatCommandText(commandText, commandParameters);
                }
            }

            return this.ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, DbConnectionOwnership.External);
        }
        
        public DbDataReader ExecuteReader(DbTransaction transaction, string storedProcName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                FormattedDbParameter[] commandParameters = this.GetStoredProcParametersFromCache(storedProcName);
                DbHelper.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteReader(transaction, CommandType.StoredProcedure, storedProcName, commandParameters);
            }

            return this.ExecuteReader(transaction, CommandType.StoredProcedure, storedProcName);
        }

        #endregion ExecuteReader

        #region ExecuteNonQuery
        
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return this.ExecuteNonQuery(commandType, commandText, (FormattedDbParameter[])null);
        }
        
        public int ExecuteNonQuery(CommandType commandType, string commandText, params FormattedDbParameter[] commandParameters)
        {
            using (DbConnection connection = this.CreateConnection())
            {
                connection.Open();

                return this.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }
        
        public int ExecuteNonQuery(string storedProcName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                FormattedDbParameter[] commandParameters = this.GetStoredProcParametersFromCache(storedProcName);
                DbHelper.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteNonQuery(CommandType.StoredProcedure, storedProcName, commandParameters);
            }
            else
            {
                return this.ExecuteNonQuery(CommandType.StoredProcedure, storedProcName);
            }
        }
        
        public int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText)
        {
            return this.ExecuteNonQuery(connection, commandType, commandText, (FormattedDbParameter[])null);
        }
        
        public int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText, params FormattedDbParameter[] commandParameters)
        {
            if (commandType == CommandType.Text)
            {
                if ((commandParameters != null) && (commandParameters.Length > 0))
                {
                    commandText = this.FormatCommandText(commandText, commandParameters);
                }
            }

            DbCommand command = connection.CreateCommand();
            DbHelper.PrepareCommand(command, connection, (DbTransaction)null, commandType, commandText, commandParameters);

            try
            {
                int retval = command.ExecuteNonQuery();
                command.Parameters.Clear();

                return retval;
            }
            catch (DbException ex)
            {
                throw this.TranslateException(ex);
            }
        }
        
        public int ExecuteNonQuery(DbConnection connection, string storedProcName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                FormattedDbParameter[] commandParameters = this.GetStoredProcParametersFromCache(storedProcName);
                DbHelper.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteNonQuery(connection, CommandType.StoredProcedure, storedProcName, commandParameters);
            }
            else
            {
                return this.ExecuteNonQuery(connection, CommandType.StoredProcedure, storedProcName);
            }
        }
        
        public int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText)
        {
            return this.ExecuteNonQuery(transaction, commandType, commandText, (FormattedDbParameter[])null);
        }
        
        public int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText, params FormattedDbParameter[] commandParameters)
        {
            if (commandType == CommandType.Text)
            {
                if ((commandParameters != null) && (commandParameters.Length > 0))
                {
                    commandText = this.FormatCommandText(commandText, commandParameters);
                }
            }

            DbCommand command = transaction.Connection.CreateCommand();
            DbHelper.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);

            try
            {
                int retval = command.ExecuteNonQuery();
                command.Parameters.Clear();

                return retval;
            }
            catch (DbException ex)
            {
                throw this.TranslateException(ex);
            }
        }
        
        public int ExecuteNonQuery(DbTransaction transaction, string storedProcName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                FormattedDbParameter[] commandParameters = this.GetStoredProcParametersFromCache(storedProcName);
                DbHelper.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteNonQuery(transaction, CommandType.StoredProcedure, storedProcName, commandParameters);
            }
            else
            {
                return this.ExecuteNonQuery(transaction, CommandType.StoredProcedure, storedProcName);
            }
        }

        #endregion ExecuteNonQuery

        #region ExecuteScalar
        
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return this.ExecuteScalar(commandType, commandText, (FormattedDbParameter[])null);
        }
        
        public object ExecuteScalar(CommandType commandType, string commandText, params FormattedDbParameter[] commandParameters)
        {
            using (DbConnection connection = this.CreateConnection())
            {
                connection.Open();

                return this.ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }
        
        public object ExecuteScalar(string storedProcName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                FormattedDbParameter[] commandParameters = this.GetStoredProcParametersFromCache(storedProcName);
                DbHelper.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteScalar(CommandType.StoredProcedure, storedProcName, commandParameters);
            }
            else
            {
                return ExecuteScalar(CommandType.StoredProcedure, storedProcName);
            }
        }
        
        public object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText)
        {
            return this.ExecuteScalar(connection, commandType, commandText, (FormattedDbParameter[])null);
        }
        
        public object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText, params FormattedDbParameter[] commandParameters)
        {
            if (commandType == CommandType.Text)
            {
                if ((commandParameters != null) && (commandParameters.Length > 0))
                {
                    commandText = this.FormatCommandText(commandText, commandParameters);
                }
            }   

            DbCommand command = connection.CreateCommand();
            DbHelper.PrepareCommand(command, connection, (DbTransaction)null, commandType, commandText, commandParameters);

            try
            {
                object retval = command.ExecuteScalar();
                command.Parameters.Clear();

                return retval;
            }
            catch (DbException ex)
            {
                throw this.TranslateException(ex);
            }
        }
        
        public object ExecuteScalar(DbConnection connection, string storedProcName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                FormattedDbParameter[] commandParameters = this.GetStoredProcParametersFromCache(storedProcName);
                DbHelper.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteScalar(connection, CommandType.StoredProcedure, storedProcName, commandParameters);
            }
            else
            {
                return this.ExecuteScalar(connection, CommandType.StoredProcedure, storedProcName);
            }
        }
        
        public object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText)
        {
            return this.ExecuteScalar(transaction, commandType, commandText, (FormattedDbParameter[])null);
        }
        
        public object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText, params FormattedDbParameter[] commandParameters)
        {
            if (commandType == CommandType.Text)
            {
                if ((commandParameters != null) && (commandParameters.Length > 0))
                {
                    commandText = this.FormatCommandText(commandText, commandParameters);
                }
            }

            DbCommand command = transaction.Connection.CreateCommand();
            DbHelper.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);

            try
            {
                object retval = command.ExecuteScalar();
                command.Parameters.Clear();

                return retval;
            }
            catch (DbException ex)
            {
                throw this.TranslateException(ex);
            }
        }

        public object ExecuteScalar(DbTransaction transaction, string storedProcName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                FormattedDbParameter[] commandParameters = this.GetStoredProcParametersFromCache(storedProcName);
                DbHelper.AssignParameterValues(commandParameters, parameterValues);

                return this.ExecuteScalar(transaction, CommandType.StoredProcedure, storedProcName, commandParameters);
            }
            else
            {
                return this.ExecuteScalar(transaction, CommandType.StoredProcedure, storedProcName);
            }
        }

        #endregion ExecuteScalar	

        #region Command & Parameters Cache & Formatting
        
        private string FormatCommandText(string commandText, params FormattedDbParameter[] parameters)
        {
            string formattedCommandText = (string)this.commandTextCache[commandText];
            if (formattedCommandText != null)
                return formattedCommandText;

            formattedCommandText = this.TranslateCommandText(commandText, parameters);
            this.commandTextCache[commandText] = formattedCommandText;

            return formattedCommandText;
        }
                
        private FormattedDbParameter[] GetStoredProcParametersFromCache(string storedProcName)
        {
            return this.GetStoredProcParametersFromCache(storedProcName, false);
        }

        private FormattedDbParameter[] GetStoredProcParametersFromCache(string storedProcName, bool includeReturn)
        {
            string hashKey = storedProcName + (includeReturn ? ":ReturnValue" : "");

            FormattedDbParameter[] cachedParameters = (FormattedDbParameter[])this.commandParametersCache[hashKey];
            if (cachedParameters == null)
            {
                DbParameter[] parameters = (DbParameter[])this.DiscoverStoredProcParameters(storedProcName, includeReturn);
                cachedParameters = new FormattedDbParameter[parameters.Length];
                int index = 0;
                foreach (DbParameter parameter in parameters)
                {
                    cachedParameters[index] = new FormattedDbParameter();
                    cachedParameters[index].DbParameter = parameter;
                    index++;
                }

                this.commandParametersCache[hashKey] = cachedParameters;
            }

            return cachedParameters;
        }
        
        protected abstract string TranslateCommandText(string commandText, params FormattedDbParameter[] parameters);
        
        protected abstract DbParameter[] DiscoverStoredProcParameters(string storedProcName, bool includeReturn);

        public FormattedDbParameter[] GetCommandParametersFromCache(string commandText)
        {
            FormattedDbParameter[] cachedParameters = (FormattedDbParameter[])this.commandParametersCache[commandText];
            if (cachedParameters == null)
                return null;

            return DbHelper.CloneParameters(cachedParameters);
        }
        
        public void SetCommandParametersToCache(string commandText, FormattedDbParameter[] dbParemeters)
        {
            this.commandParametersCache[commandText] = dbParemeters;
        }

        #endregion
    }
}
