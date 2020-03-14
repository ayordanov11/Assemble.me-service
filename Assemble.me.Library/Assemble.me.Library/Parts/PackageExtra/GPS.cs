using Assemble.me.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageExtra
{
    public class GPS : Extra
    {
        #region Constructors
        public GPS()
        {
            this.ID = 31;
            this.Name = "GPS";
            this.ProductionTime = 1;
            this.Price = 99;
            this.Description = "GPS is a navigation system which helps drivers find the shortest path to their destination.";
        }
        #endregion

        #region Methods
        public override void Add(CarModel cm)
        {
            if (!cm.Extras.OfType<GPS>().Any())
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
