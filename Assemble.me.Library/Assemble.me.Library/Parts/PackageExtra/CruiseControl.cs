using Assemble.me.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageExtra
{
    public class CruiseControl : Extra 
    {
        #region Constructors
        public CruiseControl()
        {
            this.ID = 28;
            this.Name = "Cruise Control";
            this.ProductionTime = 2;
            this.Price = 200;
            this.Description = "Cruise control allows the driver to pick a speed and leave the car alone to keep it.";
        }
        #endregion

        #region Methods
        public override void Add(CarModel cm)
        {
            if (!cm.Extras.OfType<CruiseControl>().Any())
                cm.Extras.Add(this);
            else
                throw new DuplicateExtraException(); ;
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
