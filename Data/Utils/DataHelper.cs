using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace ActividadN1.Data.Utils
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private readonly SqlConnection _connection;

        public DataHelper()
        {
             _connection = new SqlConnection(Properties.Resources.CadenaConexionLocal);
        }
        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }
        public DataTable ExecuteSPQuery(string sp, List<Parameters>? parameters = null)
        {
            DataTable dt = new DataTable();

            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                {
                    cmd.CommandType = CommandType.StoredProcedure; 
                }
                if (parameters != null)
                {
                    foreach (Parameters param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);   
                    }
                }
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }                    
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        public DataTable ExecuteSPQuery(string sp, List<Parameters>? parameters, SqlTransaction transaction)
        {
            if (transaction == null) 
                throw new ArgumentNullException(nameof(transaction));
            if (transaction.Connection == null) 
                throw new InvalidOperationException("La transacción no tiene conexión asociada.");
            DataTable dt = new DataTable();
            try
            {               
                var cmd = new SqlCommand(sp, transaction.Connection, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value ?? DBNull.Value);
                    }
                }

                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return dt;
        }

        public int ExecuteSPDML(string sp, List<Parameters>? parameters)
        {
            int rowAffected = 0;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                if (parameters != null)
                {
                    foreach (Parameters param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }
                }
                rowAffected = cmd.ExecuteNonQuery();                   
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
            return rowAffected;
        }
        public int ExecuteSPDML(string sp, List<Parameters>? parameters, SqlTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction.Connection == null) throw new InvalidOperationException("La transacción no tiene conexión asociada.");

            int rowsAffected = 0;

            try
            {
                using var cmd = new SqlCommand(sp, transaction.Connection, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value ?? DBNull.Value);
                    }
                }

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }

            return rowsAffected;
        }
        public int ExecuteSPDML(string sp, List<SqlParameter>? parameters, SqlTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction.Connection == null) throw new InvalidOperationException("La transacción no tiene conexión asociada.");

            int rowsAffected = 0;

            try
            {
                using var cmd = new SqlCommand(sp, transaction.Connection, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }

            return rowsAffected;
        }

    }
}
