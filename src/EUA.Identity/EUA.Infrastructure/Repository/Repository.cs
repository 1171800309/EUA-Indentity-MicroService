using Dapper;
using EUA.Domain.Interface;
using EUA.Infrastructure.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EUA.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected string buffer { get { return _dbContext._buffer; } }
        protected readonly DapperDbContext _dbContext;
        protected readonly ILogger<Repository<TEntity>> logger;
        public Repository(DapperDbContext dbContext, ILogger<Repository<TEntity>> logger)
        {
            this._dbContext = dbContext;
            this.logger = logger;
        }

        public virtual async Task<bool> InsertAsync(TEntity model)
        {

            if (model == null)
                throw new Exception("插入对象不能为空！");

            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("插入操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            try
            {
                DynamicParameters param = new DynamicParameters();
                string sql = GetInsertSql(model, ref param);
                int count = 0;
                using (var conn = _dbContext.GetDbConnection())
                    count = await conn.ExecuteAsync(sql, param);
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("插入表" + tableName + "对象失败！" + ex.ToString());
            }
        }

        public virtual bool Insert(TEntity model)
        {

            if (model == null)
                throw new Exception("插入对象不能为空！");

            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("插入操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            try
            {
                DynamicParameters param = new DynamicParameters();
                string sql = GetInsertSql(model, ref param);
                int count = 0;
                using (var conn = _dbContext.GetDbConnection())
                    count = conn.Execute(sql, param);
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("插入表" + tableName + "对象失败！" + ex.ToString());
            }
        }

        public virtual async Task<bool> InsertAsync(IEnumerable<TEntity> list)
        {
            if (list == null || list.Count() == 0)
                return false;

            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("批量插入表操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            StringBuilder sb = new StringBuilder();
            DynamicParameters param = new DynamicParameters();

            try
            {
                using (var conn = _dbContext.GetDbConnection())
                {
                    foreach (var obj in list)
                    {
                        sb.AppendLine(GetInsertSql(obj, ref param));
                        if (param.ParameterNames.Count() >= 1000)
                        {
                            var aa = await conn.ExecuteAsync(sb.ToString(), param);
                            sb.Clear();
                            param = new DynamicParameters();
                        }
                    }

                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        var aa = await conn.ExecuteAsync(sb.ToString(), param);
                        sb.Clear();
                        param = new DynamicParameters();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("批量插入表" + tableName + "对象失败！" + ex.ToString());
            }
        }

        public virtual bool Insert(IEnumerable<TEntity> list)
        {
            if (list == null || list.Count() == 0)
                return false;

            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("批量插入表操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            StringBuilder sb = new StringBuilder();
            DynamicParameters param = new DynamicParameters();

            try
            {
                using (var conn = _dbContext.GetDbConnection())
                {
                    foreach (var obj in list)
                    {
                        sb.AppendLine(GetInsertSql(obj, ref param));
                        if (param.ParameterNames.Count() >= 1000)
                        {
                            var aa = conn.Execute(sb.ToString(), param);
                            sb.Clear();
                            param = new DynamicParameters();
                        }
                    }

                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        var aa = conn.Execute(sb.ToString(), param);
                        sb.Clear();
                        param = new DynamicParameters();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("批量插入表" + tableName + "对象失败！" + ex.ToString());
            }
        }

        public virtual async Task<bool> RemoveAsync(string uid)
        {
            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("删除操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("DELETE FROM {0} where uid = " + buffer + "uid; ", tableName));
                int count = 0;
                using (var conn = _dbContext.GetDbConnection())
                    count = await conn.ExecuteAsync(sb.ToString(), new { uid = uid });
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("删除表" + tableName + "对象失败！" + ex);
            }
        }

        public virtual bool Remove(string uid)
        {
            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("删除操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("DELETE FROM {0} where uid = " + buffer + "uid; ", tableName));
                int count = 0;
                using (var conn = _dbContext.GetDbConnection())
                    count = conn.Execute(sb.ToString(), new { uid = uid });
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("删除表" + tableName + "对象失败！" + ex);
            }
        }

        public virtual async Task<bool> UpdateAsync(TEntity model)
        {
            if (model == null)
                throw new Exception("更新对象不能为空！");

            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("更新操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            try
            {
                DynamicParameters param = new DynamicParameters();
                using (var conn = _dbContext.GetDbConnection())
                    await conn.ExecuteAsync(GetUpdateSql(model, ref param), param);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("更新表" + tableName + "对象失败！" + ex.ToString());
            }
        }

        public virtual bool Update(TEntity model)
        {
            if (model == null)
                throw new Exception("更新对象不能为空！");

            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("更新操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            try
            {
                DynamicParameters param = new DynamicParameters();
                using (var conn = _dbContext.GetDbConnection())
                    conn.Execute(GetUpdateSql(model, ref param), param);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("更新表" + tableName + "对象失败！" + ex.ToString());
            }
        }

        public virtual async Task<bool> UpdateAsync(IEnumerable<TEntity> list)
        {
            if (list == null || list.Count() == 0)
                return false;

            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("批量更新表操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            StringBuilder sb = new StringBuilder();
            DynamicParameters param = new DynamicParameters();

            try
            {
                using (var conn = _dbContext.GetDbConnection())
                {
                    foreach (var obj in list)
                    {
                        sb.AppendLine(GetUpdateSql(obj, ref param));
                        if (param.ParameterNames.Count() >= 1000)
                        {
                            await conn.ExecuteAsync(sb.ToString(), param);
                            sb.Clear();
                            param = new DynamicParameters();
                        }
                    }

                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        await conn.ExecuteAsync(sb.ToString(), param);
                        sb.Clear();
                        param = new DynamicParameters();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("批量更新表" + tableName + "对象失败！" + ex.ToString());
            }
        }

        public virtual bool Update(IEnumerable<TEntity> list)
        {
            if (list == null || list.Count() == 0)
                return false;

            string tableName = GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("批量更新表操作未能获取到" + typeof(TEntity).FullName + "对应表名！");

            StringBuilder sb = new StringBuilder();
            DynamicParameters param = new DynamicParameters();

            try
            {
                using (var conn = _dbContext.GetDbConnection())
                {
                    foreach (var obj in list)
                    {
                        sb.AppendLine(GetUpdateSql(obj, ref param));
                        if (param.ParameterNames.Count() >= 1000)
                        {
                            conn.Execute(sb.ToString(), param);
                            sb.Clear();
                            param = new DynamicParameters();
                        }
                    }

                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        conn.Execute(sb.ToString(), param);
                        sb.Clear();
                        param = new DynamicParameters();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("批量更新表" + tableName + "对象失败！" + ex.ToString());
            }
        }

        public virtual async Task<bool> IsExistsAsync(object obj)
        {
            string tableName = GetTableName();
            PropertyInfo[] piArray = obj.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(" where 1=1 ");
            if (piArray != null)
            {
                foreach (PropertyInfo pi in piArray)
                    sb.Append($" and {pi.Name} = { buffer + pi.Name } ");
            }
            using (var conn = _dbContext.GetDbConnection())
                return await conn.ExecuteScalarAsync<bool>("select count(id) from " + tableName + sb.ToString(), obj);
        }

        public virtual bool IsExists(object obj)
        {
            string tableName = GetTableName();
            PropertyInfo[] piArray = obj.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(" where 1=1 ");
            if (piArray != null)
            {
                foreach (PropertyInfo pi in piArray)
                    sb.Append($" and {pi.Name} = { buffer + pi.Name } ");
            }
            using (var conn = _dbContext.GetDbConnection())
                return conn.ExecuteScalar<bool>("select count(id) from " + tableName + sb.ToString(), obj);
        }

        public virtual async Task<TEntity> GetAsync(string uid)
        {
            string tableName = GetTableName();
            List<TableColumn> columns = GetColumnList();
            StringBuilder queryFields = new StringBuilder();
            foreach (var column in columns)
                queryFields.Append($" { column.name } ,");
            using (var conn = _dbContext.GetDbConnection())
                return await conn.QueryFirstOrDefaultAsync<TEntity>($"select { queryFields.ToString().TrimEnd(',') } from { tableName} where Uid = @Uid", new { Uid = uid });
        }

        public virtual TEntity Get(string uid)
        {
            string tableName = GetTableName();
            List<TableColumn> columns = GetColumnList();
            StringBuilder queryFields = new StringBuilder();
            foreach (var column in columns)
                queryFields.Append($" { column.name } ,");
            using (var conn = _dbContext.GetDbConnection())
                return conn.QueryFirstOrDefault<TEntity>($"select { queryFields.ToString().TrimEnd(',') } from { tableName} where Uid = @Uid", new { Uid = uid });
        }

      

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        private string GetInsertSql(TEntity obj, ref DynamicParameters param)
        {
            string tableName = GetTableName();
            List<TableColumn> columnsList = GetColumnList(obj);
            StringBuilder insertSql = new StringBuilder();


            string columnStr = "";
            string buildColumnStr = "";
            string markString = Guid.NewGuid().ToString().Replace("-", "");
            foreach (var column in columnsList)
            {
                if (column.value == null && !column.isAllowEmpty)
                    throw new Exception("Insert:表" + tableName + "字段" + column.name + "值不能为空！");

                if (column.value != null && column.name != "Id")
                {
                    columnStr += column.name + ",";
                    buildColumnStr += buffer + column.name + markString + ",";
                    param.Add(buffer + column.name + markString, column.value.ToString());
                }
            }
            insertSql.AppendLine(string.Format(@"INSERT INTO {0}({1}) values({2});", tableName, columnStr.TrimEnd(','), buildColumnStr.TrimEnd(',')));
            return insertSql.ToString();
        }

        private string GetUpdateSql(TEntity obj, ref DynamicParameters param)
        {
            string tableName = GetTableName();
            List<TableColumn> columnsList = GetColumnList(obj);
            StringBuilder updateSql = new StringBuilder();


            string columnStr = "";
            string whereCondition = " 1=1 ";
            string markString = Guid.NewGuid().ToString().Replace("-", "");
            foreach (var column in columnsList)
            {
                if (column.value == null && column.isKey)
                    throw new Exception("Update:表" + tableName + "主键字段" + column.name + "值不能为空！");

                if (column.value != null && !column.isKey)
                {
                    columnStr += column.name + "=" + buffer + column.name + markString + ",";
                    param.Add(buffer + column.name + markString, column.value.ToString());
                }
                else if (column.isKey)
                {
                    whereCondition += " and " + column.name + "=" + buffer + column.name + markString;
                    param.Add(buffer + column.name + markString, column.value.ToString());
                }
            }
            updateSql.AppendLine(string.Format(@"UPDATE {0} SET {1} WHERE {2};", tableName, columnStr.TrimEnd(','), whereCondition));
            return updateSql.ToString();
        }

        private string GetTableName()
        {
            string tableName = "";
            var attr = typeof(TEntity).GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), true).
                FirstOrDefault() as System.ComponentModel.DataAnnotations.Schema.TableAttribute;
            if (attr != null)
            {
                tableName = attr.Name;
            }
            return tableName;
        }

        private List<TableColumn> GetColumnList(TEntity obj)
        {
            PropertyInfo[] propertys = obj.GetType().GetProperties();
            List<TableColumn> columnList = new List<TableColumn>();
            foreach (var pi in propertys)
            {
                bool isKey = false;
                bool isAllowEmpty = true;

                var isColumn = pi.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute), true).
                    FirstOrDefault() as System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;

                if (isColumn == null)
                    continue;

                var attr = pi.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true).
                    FirstOrDefault() as System.ComponentModel.DataAnnotations.KeyAttribute;

                var requireAttr = pi.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), true).
                    FirstOrDefault() as System.ComponentModel.DataAnnotations.RequiredAttribute;

                if (attr != null)
                    isKey = true;

                if (requireAttr != null)
                    isAllowEmpty = requireAttr.AllowEmptyStrings;

                columnList.Add(new TableColumn(pi.Name, pi.GetValue(obj, null), isKey, isAllowEmpty));
            }
            return columnList.OrderByDescending(t => t.isKey).ToList();
        }

        private List<TableColumn> GetColumnList()
        {
            PropertyInfo[] propertys = typeof(TEntity).GetProperties();
            List<TableColumn> columnList = new List<TableColumn>();
            foreach (var pi in propertys)
            {
                bool isKey = false;
                bool isAllowEmpty = true;

                var isColumn = pi.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute), true).
                    FirstOrDefault() as System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;

                if (isColumn == null)
                    continue;

                var attr = pi.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true).
                    FirstOrDefault() as System.ComponentModel.DataAnnotations.KeyAttribute;

                var requireAttr = pi.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), true).
                    FirstOrDefault() as System.ComponentModel.DataAnnotations.RequiredAttribute;

                if (attr != null)
                    isKey = true;

                if (requireAttr != null)
                    isAllowEmpty = requireAttr.AllowEmptyStrings;

                columnList.Add(new TableColumn(pi.Name, null, isKey, isAllowEmpty));
            }
            return columnList.OrderByDescending(t => t.isKey).ToList();
        }
        private class TableColumn
        {
            public string name { get; protected set; }

            public object value { get; protected set; }

            public bool isKey { get; protected set; }

            public bool isAllowEmpty { get; protected set; }

            public TableColumn(string name, object value, bool iskey, bool isAllowEmpty)
            {
                this.name = name;
                this.value = value;
                this.isKey = iskey;
                this.isAllowEmpty = isAllowEmpty;
            }
        }
    }

}
