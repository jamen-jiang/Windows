﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class DictionaryModifyRequest:DictionaryAddRequest
    {
        public Guid Id { get; set; }
    }
}
