using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts
{
    /// <summary>
    /// Abstract class from which all parts for a model inherit
    /// </summary>
    public abstract class CarPart
    {
        #region Fields
        private decimal price;
        private string description;
        private int productionTime;
        #endregion

        #region Properties
        // NB: Properties have protected setters to be defined within the class not by the outside world

        /// <summary>
        /// The unique identifier of the part.
        /// </summary>
        protected int ID { get; set; }

        /// <summary>
        /// Friendly string, describing the part.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The price of a car part.
        /// </summary>
        public decimal Price
        {
            get
            {
                return price;
            }
            protected set
            {
                price = value;
            }
        }
        /// <summary>
        /// A short descriptive text about the characteristics of a part.
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            protected set
            {
                description = value;
            }
        }
        /// <summary>
        /// The time it takes ot produce a certain part; measured in days.
        /// </summary>
        public int ProductionTime
        {
            get
            {
                return productionTime;
            }
            protected set
            {
                productionTime = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that assign the part to a model. Every PartId should implement it differently
        /// </summary>
        /// <param name="cm">cm stands for Current Model. This is the model the part will assign to</param>
        public abstract void Add(CarModel cm);

        /// <summary>
        /// Method that removes the the part from a model. Every PartId should implement it differently
        /// </summary>
        /// <param name="cm">cm stands for Current Model. This is the model the part will remove itself from to</param>
        public abstract void Remove(CarModel cm);

        /// <summary>
        /// Gets the unique identifier of the part.
        /// </summary>
        /// <returns>The unique identifier of the part.</returns>
        public abstract int GetId();
        #endregion
    }
}
