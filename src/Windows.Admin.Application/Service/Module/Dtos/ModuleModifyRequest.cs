﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class ModuleModifyRequest : ModuleAddRequest
    {
        public Guid Id { get; set; }
    }
}