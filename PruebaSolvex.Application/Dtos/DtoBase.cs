﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Application.Dtos
{
    public abstract class DtoBase
    {
        public DateTime ChangeDate { get; set; }

        public int ChangeUser { get; set; }

    }
}
