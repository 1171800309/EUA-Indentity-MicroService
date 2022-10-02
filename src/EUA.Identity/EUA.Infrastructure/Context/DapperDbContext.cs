using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EUA.Infrastructure.Context
{
    public class DapperDbContext : IDisposable
    {
        private readonly string _connectString;

        public readonly string _buffer;
        public DapperDbContext(string connectString)
        {
            _connectString = connectString;
            _buffer = "@";
        }

        public IDbConnection GetDbConnection()
        {
            var _conn = new MySqlConnection(_connectString);
            _conn.Open();
            return _conn;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
