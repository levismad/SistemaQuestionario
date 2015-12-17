using SQ.Core.Dominio;
//using SQ.Dominio.Interface;
using SQ.Repositorio.Interface;
using SQ.Web.Controllers.Base;
using SQ.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ.Web.Controllers.Login
{
    [LoginFilter]
    public class LoginController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Acesso()
        {
            return View();
        }

        public ActionResult SessaoErrorAjax()
        {
            return View("ErroSessao");
        }


        public ActionResult SessaoError()
        {
            return View("ErroSessaoFull");
        }

        [HttpPost]
        public ActionResult Acessar(string ebusiness, string senha)
        {
            MUsuario u = new DUsuario().ConsultarUsuario(ebusiness, senha);
            //DNotificacoes notificicoesDAO = new DNotificacoes();
            try
            {

                // var notificacoes = notificicoesDAO.ListarParcial(u);
                // string popover = "";
                // if (notificacoes.Count > 0)
                // {
                // notificacoes.ForEach(x =>
                // {
                // popover += "<div class='div-link-notificacao'><a
                //href='#' class='link-notificacao' data-tipo='" + x.Tipo + "' data-data='" +
                //x.Data + "' data-hora='" + x.Hora + "' data-_tipo='" + x._tipo + "'><b>" +
                //x.Tipo + "</b><br><p>" + x.TipoDescricao.Substring(0, 43) + "...</p>
                //</a></div><hr>";
                // });
                // }
                // popover = popover + "<p style='text-align:center'><a href='" +
                //Url.Action("Index", "Notificacoes") + "'>Ver todas notificações</a>";
                // Session["ListaNotificacoes"] = popover;
            }
            catch (Exception)
            {
                //TODO
            }
            SetUsuarioSession(u);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FinalizaSessao();
            return View("Acesso");
        }

    }
}
