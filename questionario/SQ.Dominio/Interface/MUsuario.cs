﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Dominio.Interface
{
    public class MUsuario
    {
        public string ebusiness { get; set; }
        public string senha { get; set; }
        public DateTime? DataHoraUltimoLogin { get; set; }
    }
}
