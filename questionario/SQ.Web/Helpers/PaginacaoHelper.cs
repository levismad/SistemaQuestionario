using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SQ.Web.Helpers
{
    /// <summary>
    /// Suporte para rendereizacao dos controle HTML utilizados pela paginacao.
    /// </summary>
    public static class PaginacaoHelper
    {
        private const Int32 PAGINA_ATIVA_PADRAO = 1;
        private const Int32 REGISTROS_POR_PAGINA = 10;
        private const Int32 PAGINAS_POR_CAPITULO = 5;
        /// <summary>
        /// Gera o conteudo HTML para a paginacao.
        /// </summary>
        /// <param name="html">Objeto do tipo <seealso cref="System.Web.Mvc.HtmlHelper"/> que representa a instancia extendida por estemetodo.</param>
        /// <param name="indiceLinha">Objeto do tipo <seealso cref="System.Object"/> que representa o indice da linha atual.</param>
        /// <returns>Retorna um <seealso cref="System.Web.Mvc.MvcHtmlString"/>que representa o conteudo gerado.</returns>
        public static MvcHtmlString ClassePaginacao(this HtmlHelper html, Object
       indiceLinha)
        {
            Int32 indice = Convert.ToInt32(indiceLinha);
            // pagina atual
            Int32 pagina = 1 + (indice / REGISTROS_POR_PAGINA);
            // retorno padrao
            return new MvcHtmlString(String.Format("pagina-{0} {1}", pagina, pagina == PAGINA_ATIVA_PADRAO ? null : "pagina-oculta"));
        }
        /// <summary>
        /// Gera o conteudo HTML para a paginacao.
        /// </summary>
        /// <param name="html">Objeto do tipo <seealso cref="System.Web.Mvc.HtmlHelper"/> que representa a instancia extendida por estemetodo.</param>
        /// <param name="indiceLinha">Objeto do tipo <seealso cref="System.Object"/> que representa o indice da linha atual.</param>
        /// <param name="CUSTOM_REGISTROS_POR_PAGINA">Objeto do tipo <seealso cref="System.Integer"/> que representa o total de registros por pagina.</param>
        /// <returns>Retorna um <seealso cref="System.Web.Mvc.MvcHtmlString"/>que representa o conteudo gerado.</returns>
        public static MvcHtmlString ClassePaginacao(this HtmlHelper html, Object
       indiceLinha, int CUSTOM_REGISTROS_POR_PAGINA)
        {
            Int32 indice = Convert.ToInt32(indiceLinha);
            // pagina atual
            Int32 pagina = 1 + (indice / CUSTOM_REGISTROS_POR_PAGINA);
            // retorno padrao
            return new MvcHtmlString(String.Format("pagina-{0} {1}", pagina, pagina == PAGINA_ATIVA_PADRAO ? null : "pagina-oculta"));
        }
        /// <summary>
        /// Gera o conteudo HTML para a paginacao.
        /// </summary>
        /// <param name="html">Objeto do tipo <seealso cref="System.Web.Mvc.HtmlHelper"/> que representa a instancia extendida por este metodo.</param>
        /// <param name="totalRegistros">Objeto do tipo <seealso cref="System.Object"/> que representa o total de registros.</param>
        /// <returns>Retorna um <seealso cref="System.Web.Mvc.MvcHtmlString"/>que representa o conteudo gerado.</returns>
        public static MvcHtmlString Paginacao(this HtmlHelper html, Object
       totalRegistros)
        {
            StringBuilder conteudo = new StringBuilder();
            Int32 pagina, paginas, primeira, ultima;
            // converte parametros informado
            Int32 total = Convert.ToInt32(totalRegistros);
            Int32 ativa = PAGINA_ATIVA_PADRAO;
            // define o total de paginas
            paginas = (Int32)Math.Ceiling((Double)total / REGISTROS_POR_PAGINA);
            // consiste paginacao desnecessario
            if (paginas < 2)
            {
                // como existe apenas uma pagina, nao monta o controle de paginacao
                return MvcHtmlString.Empty;
            }
            // define primeira sendo a primeira disponivel
            primeira = 1;
            // define ultima sendo a ultima disponivel (igual ao total de paginas)
            ultima = paginas;
            // consiste ativa invalida
            if (ativa < 1)
            {
                // quando pagina ativa invalida, entao a primeira pagina sera aativa
                ativa = 1;
            }
            else if (ativa > paginas)
            {
                // quando pagina ativa maior que o total depaginas, entao aultima pagina sera a ativa
                ativa = paginas;
            }
            //EDUARDO
            //// consiste quantidade de paginas maior do que o exibido
            //if( paginas > PAGINAS_POR_CAPITULO ) {
            // // redefine primeira pagina em funcao da pagina ativa
            // primeira = ativa - ( PAGINAS_POR_CAPITULO / 2 );
            //
            // // redefine ultima pagina em funcao da pagina ativa
            // ultima = ativa + ( PAGINAS_POR_CAPITULO / 2 );
            //
            // // consiste primeira pagina - primeira pagina menor que 1
            // if( primeira < 1 ) {
            //
            // // define primeira pagina como sendo a primeira disponivel
            // primeira = 1;
            //
            // // define ultima pagina como sendo a ultima pagina do capitulo (ultima disponivel)
            // ultima = PAGINAS_POR_CAPITULO;
            // }
            //


            // // consiste ultima pagina - ultimapagina maior que o total depaginas
            // if( ultima > paginas ) {
            //
            // // define ultima pagina como sendo a ultima disponivel
            // ultima = paginas;
            //
            // // define primeira pagina como sendo a ultima do capituloanterior
            // primeira = ultima - PAGINAS_POR_CAPITULO - 1;
            // }
            //}
            // abre a lista de paginas
            conteudo.AppendFormat("<ul class=\"pagination\"data-ultima-pagina=\"{0}\">", paginas);
            // consiste pagina anterior
            if (ativa == 1)
            {
                // pagina bloqueada
                //conteudo.Append( "<li class=\"prev\"><a href=\"#\"class=\"ir\">&laquo;</a></li>" );
                //EDUARDO
                conteudo.AppendFormat("<li class=\"prev\"><a href=\"#\"class=\"ir pagina\" data-pagina=\"{0}\">&laquo;</a></li>", "1");
            }
            else
            {
                // pagina habilitada
                conteudo.AppendFormat("<li class=\"prev\"><a href=\"#\"class=\"ir capitulo\" data-pagina=\"{0}\">&laquo;</a></li>", ativa - 1);
            }
            conteudo.Append("<li class=\"prev prev-capitulo capitulodisabled\"><a href=\"#\" class=\"ir\">&laquo;</a></li>");
            // define primeira pagina exibida
            pagina = primeira;
            // inicializa contador de paginas visiveis por capitulo
            Int32 capitulo = -1;
            // percorre a lista de paginas
            for (; pagina <= ultima; pagina++)
            {
                // decrementa contador
                capitulo--;
                // consiste pagina ativa
                if (pagina == ativa)
                {
                    // define o total de paginas por capitulo
                    capitulo = PAGINAS_POR_CAPITULO;
                    // define pagina
                    conteudo.AppendFormat("<li class=\"active\"><a href=\"#\"class=\"pagina\" data-pagina=\"{0}\">{1}</a></li>", pagina, pagina);
                }
                else
                {
                    // define pagina
                    conteudo.AppendFormat("<li class=\"{2}\"><a href=\"#\"class=\"pagina\" data-pagina=\"{0}\">{1}</a></li>", pagina, pagina, capitulo > 0 ? String.Empty : "hidden");


                }
            }
            conteudo.Append("<li class=\"next next-capitulo capitulo\"><ahref=\"#\" class=\"ir\">&raquo;</a></li>");
            // consiste pagina posterior
            if (ativa == paginas)
            {
                // pagina bloqueada
                conteudo.Append("<li class=\"next\"><a href=\"#\"class=\"ir\">&raquo;</a></li>");
            }
            else
            {
                // pagina habilitada
                conteudo.AppendFormat("<li class=\"next\"><a href=\"#\"class=\"ir pagina\" data-pagina=\"{0}\">&raquo;</a></li>", paginas);
            }
            // fecha a lista de paginas
            conteudo.Append("</ul>");
            // retorno padrao
            return new MvcHtmlString(conteudo.ToString());
        }
        /// <summary>
        /// Gera o conteudo HTML para a paginacao customizada.
        /// </summary>
        /// <param name="html">Objeto do tipo <seealso cref="System.Web.Mvc.HtmlHelper"/> que representa a instancia extendida por este metodo.</param>
        /// <param name="totalRegistros">Objeto do tipo <seealso cref="System.Object"/> que representa o total de registros.</param>
        /// <param name="CUSTOM_REGISTROS_POR_PAGINA">Objeto do tipo <seealso cref="System.Integer"/> que representa o total de registros por pagina.</param>
        /// <returns>Retorna um <seealso cref="System.Web.Mvc.MvcHtmlString"/>que representa o conteudo gerado.</returns>
        public static MvcHtmlString Paginacao(this HtmlHelper html, Object
       totalRegistros, int CUSTOM_REGISTROS_POR_PAGINA, string id_paginacao = "")
        {
            StringBuilder conteudo = new StringBuilder();
            Int32 pagina, paginas, primeira, ultima;
            // converte parametros informado
            Int32 total = Convert.ToInt32(totalRegistros);
            Int32 ativa = PAGINA_ATIVA_PADRAO;
            // define o total de paginas
            paginas = (Int32)Math.Ceiling((Double)total /
           CUSTOM_REGISTROS_POR_PAGINA);
            // consiste paginacao desnecessario
            if (paginas < 2)
            {
                // como existe apenas uma pagina, nao monta o controle de paginacao
                return MvcHtmlString.Empty;
            }
            // define primeira sendo a primeira disponivel
            primeira = 1;


            // define ultima sendo a ultima disponivel (igual ao total de paginas)
            ultima = paginas;
            // consiste ativa invalida
            if (ativa < 1)
            {
                // quando pagina ativa invalida, entao a primeira pagina sera a ativa
                ativa = 1;
            }
            else if (ativa > paginas)
            {
                // quando pagina ativa maior que o total depaginas, entao a ultima pagina sera a ativa
                ativa = paginas;
            }
            //EDUARDO
            //// consiste quantidade de paginas maior do que o exibido
            //if( paginas > PAGINAS_POR_CAPITULO ) {
            // // redefine primeira pagina em funcao da pagina ativa
            // primeira = ativa - ( PAGINAS_POR_CAPITULO / 2 );
            //
            // // redefine ultima pagina em funcao da pagina ativa
            // ultima = ativa + ( PAGINAS_POR_CAPITULO / 2 );
            //
            // // consiste primeira pagina - primeira pagina menor que 1
            // if( primeira < 1 ) {
            //
            // // define primeira pagina como sendo a primeira disponivel
            // primeira = 1;
            //
            // // define ultima pagina como sendo a ultima pagina do capitulo (ultima disponivel)
            // ultima = PAGINAS_POR_CAPITULO;
            // }
            //
            // // consiste ultima pagina - ultimapagina maior que o total de paginas
            // if( ultima > paginas ) {
            //
            // // define ultima pagina como sendo a ultima disponivel
            // ultima = paginas;
            //
            // // define primeira pagina como sendo a ultima do capitulo anterior
            // primeira = ultima - PAGINAS_POR_CAPITULO - 1;
            // }
            //}
            // abre a lista de paginas
            conteudo.AppendFormat("<ul class=\"pagination\" id=\"" + id_paginacao + "\" data-ultima-pagina=\"{0}\">", paginas);
            // consiste pagina anterior
            if (ativa == 1)
            {
                // pagina bloqueada
                //conteudo.Append( "<li class=\"prev\"><a href=\"#\"class=\"ir\">&laquo;</a></li>" );
                //EDUARDO
                conteudo.AppendFormat("<li class=\"prev\"><a href=\"#\"class=\"ir pagina\" data-pagina=\"{0}\">&laquo;</a></li>", "1");
            }
            else
            {
                // pagina habilitada
                conteudo.AppendFormat("<li class=\"prev\"><a href=\"#\"class=\"ir capitulo\" data-pagina=\"{0}\">&laquo;</a></li>", ativa - 1);
            }
            conteudo.Append("<li class=\"prev prev-capitulo capitulo disabled\"><a href=\"#\" class=\"ir\">&laquo;</a></li>");
            // define primeira pagina exibida
            pagina = primeira;
            // inicializa contador de paginas visiveis por capitulo
            Int32 capitulo = -1;
            // percorre a lista de paginas
            for (; pagina <= ultima; pagina++)
            {
                // decrementa contador
                capitulo--;
                // consiste pagina ativa
                if (pagina == ativa)
                {
                    // define o total de paginas por capitulo
                    capitulo = PAGINAS_POR_CAPITULO;
                    // define pagina
                    conteudo.AppendFormat("<li class=\"active\"><a href=\"#\" class=\"pagina\" data-pagina=\"{0}\">{1}</a></li>", pagina, pagina);
                }
                else
                {
                    // define pagina
                    conteudo.AppendFormat("<li class=\"{2}\"><a href=\"#\" class=\"pagina\" data-pagina=\"{0}\">{1}</a></li>", pagina, pagina, capitulo > 0
                   ? String.Empty : "hidden");
                }
            }
            conteudo.Append("<li class=\"next next-capitulo capitulo\"><a href=\"#\" class=\"ir\">&raquo;</a></li>");
            // consiste pagina posterior
            if (ativa == paginas)
            {
                // pagina bloqueada
                conteudo.Append("<li class=\"next\"><a href=\"#\" class=\"ir\">&raquo;</a></li>");
            }
            else
            {
                // pagina habilitada
                conteudo.AppendFormat("<li class=\"next\"><a href=\"#\"class=\"ir pagina\" data-pagina=\"{0}\">&raquo;</a></li>", paginas);
            }
            // fecha a lista de paginas
            conteudo.Append("</ul>");
            // retorno padrao
            return new MvcHtmlString(conteudo.ToString());
        }


    }

}
