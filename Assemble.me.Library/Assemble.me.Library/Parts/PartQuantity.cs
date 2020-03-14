using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts
{
    /// <summary>
    /// Middle class that provides information about parts' quantity.
    /// </summary>
    public class PartQuantity
    {
        #region Properties
        public int PartId { get; private set; }
        public int Quantity { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        #endregion

        #region Constructors
        public PartQuantity(int partId, int quantity, string name, string type)
        {
            this.PartId = partId;
            this.Quantity = quantity;
            this.Type = type;
            this.Name = name;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Assigns a new quantity for the given car part.
        /// </summary>
        /// <param name="quantity">The quantity that should be purchased.</param>
        public void Restock(int quantity)
        {
            this.PurchaseStock(quantity);
            this.Quantity += quantity;
        }

        /// <summary>
        /// Increases the quantity of the part.
        /// </summary>
        /// <param name="quantity">The quantity to be added.</param>
        private void PurchaseStock(int quantity)
        {
            // Open the connection
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {
                con.Open();

                // Update the quantity for the PartId
                using (MySqlCommand cmd = new MySqlCommand(
                    "UPDATE `inventory` SET `quantity`=`quantity`+ @q WHERE `part_id` = @id", con))
                {
                    cmd.Parameters.AddWithValue("@q", quantity);
                    cmd.Parameters.AddWithValue("@id", PartId);

                    cmd.ExecuteNonQuery();
                }

                // Close the connection
                con.Close();
            }
        }

        #endregion
    }
}
