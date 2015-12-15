using SQ.Core.Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Repositorio.Interface
{
    public class DUsuario
    {
        public MUsuario ConsultarUsuario(string business, string senha)
        {
            var usuarioValidos = ConfigurationManager.AppSettings["UsuariosLiberados"];
            var idUsuario = usuarioValidos.IndexOf(business.ToLower());
            if (idUsuario == -1)
            {
                throw new Exception("Login Inválido");
            }

            MUsuario usuario = new MUsuario();
            usuario.DataHoraUltimoLogin = DateTime.Now;
            usuario.ebusiness = usuarioValidos[idUsuario].ToString();
            usuario.senha = "";

            return usuario;

        }
    }
}
