using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Admin.Application
{
    public class OrganizationModifyRequest : OrganizationAddRequest
    {
        public Guid Id { get; set; }
    }
}
