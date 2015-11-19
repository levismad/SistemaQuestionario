﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Services.Contratos.GestaoFluxo
{
    public interface IServicoFluxo
    {
        void ExecutarAcaoFluxo();
        void IniciarAcaoFluxo();
        void RetomarAcaoFluxoAnterior();
    }
}
