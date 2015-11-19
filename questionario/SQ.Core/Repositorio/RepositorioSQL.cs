using ServiceStack.OrmLite;
using SQ.Core.Dominio;
using SQ.Core.Repositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Core.Database
{

    /// <summary>
    /// Repositório base para conexão à base SQL
    /// </summary>
    public class RepositorioSQL<T> : IRepositorio<T> where T : IEntidade, new()
    {
        public RepositorioSQL(string connection)
        {
            connectionString = connection;
            OrmLiteConfig.DialectProvider = SqlServerDialect.Provider;
        }
        public RepositorioSQL(SqlConnection connection)
        {
            db = connection;
            OrmLiteConfig.DialectProvider = SqlServerDialect.Provider;
        }
        protected SqlConnection db;
        protected RepositorioSQL<T2> RepositorioAgregado<T2>() where T2 :
       IEntidade, new()
        {
            if (db == null)
            {
                db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
            }
            return new RepositorioSQL<T2>(db);
        }
        protected string connectionString;
        public T ObterPorId(int Id, bool keepOpen = false)
        {
            try
            {
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                //var retorno = db.QueryById<T>(Id);
                if (!keepOpen)
                {
                    db.Close();
                }
                //return retorno;
                return new T { };
            }
            catch (Exception)
            {
                throw;


            }
        }
        public List<T> Listar(Expression<Func<T, bool>> filtro = null, bool
       keepOpen = false)
        {
            List<T> retorno = new List<T>();
            try
            {
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                if (filtro == null)
                {
                    //retorno = db.Select<T>();
                }
                else
                {
                    //retorno = db.SelectParam<T>(filtro);
                }
                if (!keepOpen)
                {
                    db.Close();
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<T> ExecutarSQL(string sql, object parametros = null, bool
       keepOpen = false)
        {
            List<T> retorno = new List<T>();
            try
            {
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                if (parametros == null)
                {
                    //retorno = db.SqlList<T>(sql);
                }
                else
                {
                    //retorno = db.SqlList<T>(sql, parametros);
                }
                if (!keepOpen)
                {
                    db.Close();


                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected List<T2> ExecutarSQLCustomizado<T2>(string sql, object
       parametros = null, bool keepOpen = false) where T2 : new()
        {

            List<T2> retorno = new List<T2>();
            try
            {
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                if (parametros == null)
                {
                    //retorno = db.SqlList<T2>(sql);
                }
                else
                {
                    //retorno = db.SqlList<T2>(sql, parametros);
                }
                if (!keepOpen)
                {
                    db.Close();
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ExecutarSQLVoid(string sql, bool keepOpen = false)
        {
            try
            {
                if (db == null)
                {

                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                //db.ExecuteSql(sql);
                if (!keepOpen)
                {
                    db.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Inserir(T objeto, bool keepOpen = false, bool hasID = true)
        {
            try
            {
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                if (hasID)
                {
                   // var ev =
                   //OrmLiteConfig.DialectProvider.ExpressionVisitor<T>();
                   // ev.Select(r => Sql.As(Sql.Max(r.Id), "Id"));
                   // var MaxId = db.Scalar<int>(ev.SelectExpression);
                   // objeto.Id = MaxId + 1;
                }
                //db.Insert<T>(objeto);
                if (!keepOpen)
                {
                    db.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Atualizar(T objeto, bool keepOpen = false)
        {
            try
            {
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                //db.Update<T>(objeto);
                if (!keepOpen)
                {
                    db.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Excluir(T objeto, bool keepOpen = false)
        {
            try
            {
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                //db.Delete<T>(objeto);
                if (!keepOpen)
                {
                    db.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<List<List<object>>> ExecutarSQLToJson(string sql, bool
       keepOpen = false)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                SqlDataReader dr;
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Connection = db;
                dr = command.ExecuteReader();
                List<List<List<object>>> result = new
               List<List<List<object>>>();
                while (dr.Read())
                {
                    int fieldsCount = dr.FieldCount;
                    List<List<object>> field = new List<List<object>>();
                    for (int i = 0; i < fieldsCount; i++)
                    {
                        List<object> detail = new List<object>();
                        detail.Add(dr.GetName(i));
                        detail.Add(dr[i]);
                        field.Add(detail);
                    }
                    result.Add(field);
                }
                if (!keepOpen)
                {
                    db.Close();
                }
                return result;


            }
            catch (Exception ex)
            {
                List<List<List<object>>> result = new
               List<List<List<object>>>();
                List<List<object>> field = new List<List<object>>();
                List<object> detail = new List<object>();
                detail.Add("Erro");
                detail.Add(ex.Message);
                field.Add(detail);
                result.Add(field);
                return result;
            }
        }
        public string ExecutarSQLToJsonNonQuery(string sql, bool keepOpen =
       false)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                int affectedRows;
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Connection = db;
                affectedRows = command.ExecuteNonQuery();
                if (!keepOpen)
                {
                    db.Close();
                }
                return string.Format("({0} row(s) affected)", affectedRows);
            }
            catch (Exception ex)
            {
                return string.Format("(None row(s) affected {0}).\n",
               ex.Message);
            }
        }
        public int ExecutarSQLSeguro(string sql, SqlCommand command, bool
       keepOpen = false)
        {
            int retorno;
            try
            {
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                SqlTransaction transaction = db.BeginTransaction();


                try
                {
                    SqlParameterCollection parameters;
                    parameters = command.Parameters;
                    command = new SqlCommand(sql, db, transaction);
                    if (parameters.Count > 0)
                    {
                        foreach (SqlParameter x in parameters)
                        {
                            command.Parameters.AddWithValue(x.ParameterName,
                           x.Value);
                        }
                    }
                    retorno = command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                if (!keepOpen)
                {
                    db.Close();
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ExecutarSQLSeguro(string sql, List<SqlCommand> commands,
       bool keepOpen = false)
        {
            int retorno = 999;
            try
            {
                if (db == null)
                {
                    db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
                }
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                SqlTransaction transaction = db.BeginTransaction();
                try
                {
                    commands.ForEach(command =>
                    {
                        SqlParameterCollection parameters;
                        parameters = command.Parameters;
                        command = new SqlCommand(sql, db, transaction);
                        if (parameters.Count > 0)
                        {
                            foreach (SqlParameter x in parameters)
                            {
                                command.Parameters.AddWithValue(x.ParameterName,
                               x.Value);
                            }


                        }
                        command.ExecuteNonQuery();
                    });
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                if (!keepOpen)
                {
                    db.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SqlBulkCopy SqlBulkCopy(SqlTransaction transaction)
        {
            return new SqlBulkCopy(db, SqlBulkCopyOptions.Default, transaction);
        }
        public SqlTransaction CreateTransaction()
        {
            if (db == null)
            {
                db = new SqlConnection();//ConexaoBD().criaConexaoSeguraSQLServer(connectionString);
            }
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }
            SqlTransaction transaction = db.BeginTransaction();
            return transaction;
        }
    }
}
