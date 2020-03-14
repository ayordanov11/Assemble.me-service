using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Exceptions
{
    /// <summary>
    /// A class describing a custome exception which occurs when an extra is added more than once
    /// to a car model.
    /// </summary>
    public class DuplicateExtraException : Exception
    {
        public DuplicateExtraException()
            : base("This car model already contains an extra of this type.")
        {

        }
    }
}
