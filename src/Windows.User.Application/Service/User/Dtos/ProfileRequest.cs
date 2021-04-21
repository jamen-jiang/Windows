using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.User.Application
{
    public class ProfileRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public int? Gender { get; set; }
    }
}
