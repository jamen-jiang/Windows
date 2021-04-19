using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Infrastructure.Extensions
{
    public static class IntExtension
    {
        public static int ToInt(this object obj)
        {
            if (obj == null)
                return 0;
            int.TryParse(obj.ToString(), out int _number);
            return _number;
        }
    }
}
