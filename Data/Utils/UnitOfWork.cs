using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadN1.Data.Utils
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;        
        public SqlTransaction Transaction => _transaction ?? throw new InvalidOperationException("La transacción no está disponible (¿ya se hizo SaveChanges/Dispose?).");

        public UnitOfWork()
        {
            _connection = new SqlConnection(Properties.Resources.CadenaConexionLocal);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void SaveChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }           
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
