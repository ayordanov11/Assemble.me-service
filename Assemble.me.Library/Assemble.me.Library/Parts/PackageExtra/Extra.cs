using Assemble.me.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageExtra
{
    public class Extra : CarPart
    {
        #region Constructors
        public Extra()
        {

        }
        #endregion

        #region Methods
        public override void Add(CarModel cm)
        {
            if (!cm.Extras.OfType<Extra>().Any())
                cm.Extras.Add(this);
            else
                throw new DuplicateExtraException();
        }

        public override void Remove(CarModel cm)
        {
            cm.Extras.RemoveAll(x => x.GetType().Name == this.GetType().Name);
        }
        public override int GetId()
        {
            throw new InvalidOperationException("Can't get the id of a unspecified extra.");
        }
        #endregion

    }
}
