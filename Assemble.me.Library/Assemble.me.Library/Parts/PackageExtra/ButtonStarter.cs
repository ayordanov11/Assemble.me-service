using Assemble.me.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageExtra
{
    public class ButtonStarter : Extra
    {
        #region Constructors
        public ButtonStarter()
        {
            this.ID = 27;
            this.Name = "Button Starter";
        }
        #endregion

        #region Methods
        public override void Add(CarModel cm)
        {
            if (!cm.Extras.OfType<ButtonStarter>().Any())
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
            return this.ID;
        }
        #endregion
    }
}
