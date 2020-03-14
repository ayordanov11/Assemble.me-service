using Assemble.me.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageExtra
{
    public class AC : Extra
    {
        #region Constructors
        public AC()
        {
            this.ID = 26;
            this.Name = "AC";
            this.ProductionTime = 1;
            this.Price = 100;
            this.Description = "AC stands for Air Conditioning. It is a great tool for dealing with the high summer temperature and the cold winter weather.";
        }
        #endregion

        #region Methods
        public override void Add(CarModel cm)
        {
            if (!cm.Extras.OfType<AC>().Any())
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
