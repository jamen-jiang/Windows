using System;
using Windows.Admin.Domain.Enums;
using Windows.SeedWork;

namespace Windows.Admin.Domain
{
    public partial class Privilege : Entity
    {
        public Privilege()
        { }
        public Privilege(MasterEnum master, int masterValue, AccessEnum access, int accessValue)
        {
            Master = master.ToString();
            MasterValue = masterValue;
            Access = access.ToString();
            AccessValue = accessValue;
        }
        public string Master { get; set; }
        public int MasterValue { get; set; }
        public string Access { get; set; }
        public int AccessValue { get; set; }
        public int Operation { get; set; }
    }
}
