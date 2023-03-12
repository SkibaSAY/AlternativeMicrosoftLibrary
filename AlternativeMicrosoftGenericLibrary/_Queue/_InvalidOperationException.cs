using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternativeMicrosoftGenericLibrary
{
    public class _InvalidOperationException:Exception
    {
        public _InvalidOperationException(string message):base(message)
        {

        }
    }
}
