﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataTransferObjects
{
    public class DTOBaseResponse
    {
        public DTOUsuario Usuario { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
