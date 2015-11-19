using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Core.Enums
{
    public enum PapelFluxo
    {
        [Description("Respondente")]
        Respondente = 1,
        [Description("Aprovador")]
        Aprovador = 2,
        [Description("Ponto Focal")]
        PontoFocal = 3,
        [Description("Cadastro")]
        Cadastro = 4
    }
}
