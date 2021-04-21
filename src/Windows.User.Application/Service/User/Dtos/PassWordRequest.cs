using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.User.Application
{
    public class PassWordRequest
    {
        public string OldPassWord { get; set; }
        public string NewPassWord { get; set; }
        public string NewPassWordConfirm { get; set; }
    }
}
