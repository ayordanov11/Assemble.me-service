using Assemble.me.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageExtra
{
    public class Parktronic : Extra
    {
        #region Constructors
        public Parktronic()
        {
            this.ID = 29;
            this.Name = "Parktronic";
            this.ProductionTime = 2;
            this.Price = 200;
            this.Description = "Parktronic gives the car the ability to parc automatically without the driver's interference";
        }
        #endregion

        #region Methods
        public override void Add(CarModel cm)
        {
            if (!cm.Extras.OfType<Parktronic>().Any())
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
