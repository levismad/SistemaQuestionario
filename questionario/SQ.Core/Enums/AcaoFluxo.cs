using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Core.Enums
{
    public enum AcaoFluxo
    {
        [Description("Responder Questionário")]
        ResponderQuestionario = 1,
        [Description("Aprovar Questionário")]
        AprovarQuestionario = 2
    }
}
