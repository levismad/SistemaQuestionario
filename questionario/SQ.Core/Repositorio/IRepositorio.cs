using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Core.Repositorio
{
    public interface IRepositorio<T>
    {
        T ObterPorId(int ID, bool keepOpen = false);
        List<T> Listar(Expression<Func<T, bool>> filtro, bool keepOpen = false);
        List<T> ExecutarSQL(string sql, object parametros, bool keepOpen = false);
        int ExecutarSQLSeguro(string sql, SqlCommand command, bool keepOpen = false);
        void ExecutarSQLSeguro(string sql, List<SqlCommand> command, bool keepOpen = false);
        SqlBulkCopy SqlBulkCopy(SqlTransaction transaction);
        SqlTransaction CreateTransaction();
        void Inserir(T objeto, bool keepOpen = false, bool hasID = true);
        void Atualizar(T objeto, bool keepOpen = false);
        void Excluir(T objeto, bool keepOpen = false);

    }
}
