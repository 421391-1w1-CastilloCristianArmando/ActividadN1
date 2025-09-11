using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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


        public DataTable ExecuteSPQuery(string sp, List<Parameters>? parameters = null, SqlTransaction? transaction = null)
        {
            var dt = new DataTable();
            if (transaction != null)
            {
                if (transaction.Connection == null)
                    throw new InvalidOperationException("La transacción no tiene conexión asociada.");

                using var cmd = new SqlCommand(sp, transaction.Connection, transaction)
                { CommandType = CommandType.StoredProcedure };

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);

                using var rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                return dt;
            }           

            using (var cmd = new SqlCommand(sp, _connection)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);

                _connection.Open();
                using var rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                _connection.Close();
                return dt;

            }
        }        
        

        public int ExecuteSPDML(string sp, List<Parameters>? parameters = null, SqlTransaction? transaction = null)
        {
            if (transaction != null)
            {
                if (transaction.Connection == null)
                    throw new InvalidOperationException("La transacción no tiene conexión asociada.");

                using var cmd = new SqlCommand(sp, transaction.Connection, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);

                return cmd.ExecuteNonQuery();
            }
            _connection.Open();
            using var cmd2 = new SqlCommand(sp, _connection) { CommandType = CommandType.StoredProcedure };

            if (parameters != null)
                foreach (var p in parameters)
                    cmd2.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);

            int row = cmd2.ExecuteNonQuery();
            _connection.Close();
            return row;
        }

        public int ExecuteSPDML(string sp, List<SqlParameter>? sqlParams = null, SqlTransaction? transaction = null)
        {
            if (transaction != null)
            {
                if (transaction.Connection == null) 
                    throw new InvalidOperationException("La transacción no tiene conexión asociada.");
                using var cmd = new SqlCommand(sp, transaction.Connection, transaction) 
                { 
                    CommandType = CommandType.StoredProcedure 
                };
                if (sqlParams != null && sqlParams.Count > 0) 
                    cmd.Parameters.AddRange(sqlParams.ToArray());

                return cmd.ExecuteNonQuery();
            }

            _connection.Open();
            using var cmd2 = new SqlCommand(sp, _connection) 
            { 
                CommandType = CommandType.StoredProcedure 
            };
            if (sqlParams != null && sqlParams.Count > 0) 
                cmd2.Parameters.AddRange(sqlParams.ToArray());
            
            int row = cmd2.ExecuteNonQuery();
            _connection.Close();

            return row;
        }


    }
}
