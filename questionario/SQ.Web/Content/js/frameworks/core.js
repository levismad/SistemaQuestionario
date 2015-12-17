//headers: { 'x-my-custom-header': 'some value' }
var Carregando, PararCarregando, alerta, posAlertaOk;
$(function () {
    $.ajaxSetup({
        cache: false
    });
    $("#botaoLogoff").click(function () {
        return location.href = "../Default.aspx?logoff=1";
    });
    $("#botaoHome").click(function () {
        return location.href = "../Default.aspx";
    });
    jQuery.fn.paginacao = function (options) {
        var cssClass, htmlPager, idObjeto, page, pages, primeiraQuestao, _i;
        page = options.response.getResponseHeader("X-Current-Page");
        pages = options.response.getResponseHeader("X-Total-Pages");
        htmlPager = [];
        idObjeto = this.attr("id");
        this.html("");
        if (pages > 1) {
            htmlPager.push("<table><tr>");
            htmlPager.push("<td>Páginas:</td>");
            primeiraQuestao = 1;
            if (page > 4) {
                primeiraQuestao = page - 3;
            }
            if (primeiraQuestao > 1) {
                htmlPager.push("<td class=\"page-first-" + idObjeto + "\" page=\"\" title=\"Primeira página\">...</td>");
            }
            for (_i = primeiraQuestao; primeiraQuestao <= pages ? _i <= pages : _i >= pages; primeiraQuestao <= pages ? _i++ : _i--) {
                cssClass = "pager-" + idObjeto;
                if (_i === +page) {
                    cssClass = "pager-active-" + idObjeto;
                }
                htmlPager.push("<td class=\"" + cssClass + "\" page=\"" + _i + "\">" + _i + "</td>");
                if (_i > primeiraQuestao + 5) {
                    htmlPager.push("<td class=\"page-last-" + idObjeto + "\" page=\"\" title=\"Última página\">...</td>");
                    _i = pages + 1;
                }
            }
            htmlPager.push("</tr></table>");
            this.html(htmlPager.join(''));
            $(".pager-" + idObjeto).css("cursor", "pointer");
            $(".page-first-" + idObjeto).css("cursor", "pointer");
            $(".page-last-" + idObjeto).css("cursor", "pointer");
            $(".pager-" + idObjeto).css("background-color", "#EEE");
            $(".pager-active-" + idObjeto).css("font-weight", "bold");
            $(".pager-" + idObjeto).click(function () {
                return options.fetch($(this).attr("page"));
            });
            $(".page-first-" + idObjeto).click(function () {
                return options.fetch(1);
            });
            $(".page-last-" + idObjeto).click(function () {
                return options.fetch(pages);
            });
            return false;
        }
    };
    $("#alert-dialog").dialog({
        resizable: false,
        autoOpen: false,
        height: 150,
        width: 500,
        modal: true,

        buttons: {
            "OK": function () {
                $(this).dialog("close");
                posAlertaOk.pos();
                return true;
            }
        }
    });
    return false;
});
posAlertaOk = {
    pos: function () { }
};
alerta = function (mensagem, options) {
    if (options == null) {
        options = {
            pos: function () { }
        };
    }
    $("#textoAlertaDialogo").text(mensagem);
    $("#alert-dialog").dialog("open");
    posAlertaOk = options;
    return false;
};
jQuery.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px");
    this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px");
    return this;
};
Carregando = function (mensagem) {
    $("#loading>span").text(mensagem);
    $("#loading").center();
    return $("#loading").show();
};
PararCarregando = function () {
    return $("#loading").hide();
};
String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
};
$(document).ajaxStart(function () {
    Carregando();
});
$(document).ajaxComplete(function () {
    PararCarregando();
});
var clonarControle = function (controleOriginal, controleNovo, controleLocalizacao, boolCopiarValorAtual,
  boolInserirDentro, boolInserirAntes, boolInserirDepois) {
    if (boolInserirDentro) {
        $('#' + controleOriginal).clone().attr('id', controleNovo).appendTo('#' + controleLocalizacao);
    } else if (boolInserirAntes) {
        $('#' + controleOriginal).clone().attr('id', controleNovo).insertAfter('#' + controleLocalizacao);
    } else { //boolInserirDepois
        $('#' + controleOriginal).clone().attr('id', controleNovo).insertBefore('#' + controleLocalizacao);
    }

    if (boolCopiarValorAtual) {
        var idNovo = '#' + controleNovo;
        var idOriginal = '#' + controleOriginal;
        var ValorOriginal = $(idOriginal).val().trim();
        $(idNovo).val(ValorOriginal);
    }
};

function isDate(vpExpression) {
    if (vpExpression == null || vpExpression == "") {
        return "sucesso";
    }
    var dateStr = new String(vpExpression)
    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var matchArray = dateStr.match(datePat); // is the format ok?
    if (matchArray == null) {
        return "O formato da data está inválido";

    }
    day = matchArray[1];
    month = matchArray[3];
    year = matchArray[5];
    //Verifica o mês
    if (month < 1 || month > 12) {
        return "Mês inválido";

    }
    if (day < 1 || day > 31) {
        return "Dia inválido";

    }
    if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
        return "O mês " + month + " não possui 31 dias"

    }
    //Checa fevereiro
    if (month == 2) {
        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        if (day > 29) {
            return "Fevereiro não possui " + day + " dias.";

        } else if (day == 29 && !isleap) {
            return "Fevereiro de " + year + " não possui o dia 29.";

        }
    }
    //checa Ano
    if (year < 1900 || year > 2400) {
        return "Ano inválido";

    }
    return "sucesso";
}

function CompararData(dataincio, datafim) {
    if (dataincio == null || dataincio == "" || datafim == null || datafim == "") {
        return false;
    }

    var dateStrincio = new String(dataincio);
    var datePatincio = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var matchArrayincio = dateStrincio.match(datePatincio); // is the format ok?
    var dateStrfim = new String(datafim);
    var datePatfim = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var matchArrayfim = dateStrfim.match(datePatfim); // is the format ok?
    var dayincio = matchArrayincio[1];
    var monthincio = matchArrayincio[3];
    var yearincio = matchArrayincio[5];
    var dayfim = matchArrayfim[1];
    var monthfim = matchArrayfim[3];
    var yearfim = matchArrayfim[5];
    if (new Date(monthincio + "/" + dayincio + "/" + yearincio) > (new Date(monthfim + "/" + dayfim + "/" +
        yearfim))) {
        return true;
    }
    return false;

}
