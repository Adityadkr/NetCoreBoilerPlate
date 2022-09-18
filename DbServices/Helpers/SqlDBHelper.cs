using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DbServices.Helpers
{
    public class SqlDBHelper
    {
        private readonly IConfiguration _config;
        private static SqlConnection connection;
        private static string connectionString;
        public SqlDBHelper(IConfiguration config)
        {
            _config = config;
            connectionString = _config.GetConnectionString("defaultConnection").ToString();
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            connection.Open();
        }
        public void CloseConnection()
        {
            connection.Close();
        }
        public SqlCommand GetSqlCommand(string query, CommandType commandType)
        {

            var command = new SqlCommand(query, connection);
            command.CommandType = commandType;
            return command;
        }
        public SqlDataAdapter GetSqlDataAdapter(SqlCommand cmd)
        {
            return new SqlDataAdapter(cmd);
        }

        #region DataTable
        public DataTable GetDatatable(string query, CommandType commandType, List<SqlParameter> parameters)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = GetSqlCommand(query, commandType);
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataAdapter adapter = GetSqlDataAdapter(cmd);
                OpenConnection();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataTable GetDatatable(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = GetSqlCommand(query, CommandType.Text);
                SqlDataAdapter adapter = GetSqlDataAdapter(cmd);
                OpenConnection();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region DataSet
        public DataSet GetDataSet(string query, CommandType commandType, List<SqlParameter> parameters)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = GetSqlCommand(query, commandType);
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataAdapter adapter = GetSqlDataAdapter(cmd);
                OpenConnection();
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataSet GetDataSet(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = GetSqlCommand(query, CommandType.Text);
                SqlDataAdapter adapter = GetSqlDataAdapter(cmd);
                OpenConnection();
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region ExecuteNonQuery

        public int ExecuteNonQuery(string query)
        {
            try
            {
              
                SqlCommand cmd = GetSqlCommand(query, CommandType.Text);
               
                OpenConnection();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int ExecuteNonQuery(string query, CommandType commandType, List<SqlParameter> parameters)
        {
            try
            {

                SqlCommand cmd = GetSqlCommand(query, CommandType.Text);
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                OpenConnection();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region ExecuteScalar

        public object ExecuteScalar(string query)
        {
            try
            {

                SqlCommand cmd = GetSqlCommand(query, CommandType.Text);
                OpenConnection();
                object i = cmd.ExecuteScalar();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public object ExecuteScalar(string query, CommandType commandType, List<SqlParameter> parameters)
        {
            try
            {

                SqlCommand cmd = GetSqlCommand(query, CommandType.Text);
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                OpenConnection();
                object i = cmd.ExecuteScalar();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region ExecuteReader

        public SqlDataReader ExecuteReader(string query)
        {
            try
            {

                SqlCommand cmd = GetSqlCommand(query, CommandType.Text);
                OpenConnection();
                SqlDataReader i = cmd.ExecuteReader();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public SqlDataReader ExecuteReader(string query, CommandType commandType, List<SqlParameter> parameters)
        {
            try
            {

                SqlCommand cmd = GetSqlCommand(query, CommandType.Text);
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                OpenConnection();
                SqlDataReader i = cmd.ExecuteReader();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion
    }
}
