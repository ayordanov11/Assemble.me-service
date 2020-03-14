using Assemble.me.Library.Parts;
using Assemble.me.Library.Parts.PackageChassis;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Assemble.me.Library
{
    public class Inventory
    {
        #region Properties
        private static List<PartQuantity> AvailableParts { get; set; }
        private static List<PartQuantity> UnavailableParts { get; set; }
        private static List<PartQuantity> AllParts { get; set; }
        #endregion

        #region Methods

        #region Administrative Methods

        /// <summary>
        /// Retrieves all car parts with positive quantity in the inventory.
        /// </summary>
        /// <returns>A list of parts</returns>
        public static List<PartQuantity> GetAvailableParts()
        {
            SyncWithDb();
            return AvailableParts;
        }

        /// <summary>
        /// Retrieves all car parts with negative quantity in the inventory.
        /// </summary>
        /// <returns>A list of parts</returns>
        public static List<PartQuantity> GetUnavailableParts()
        {
            SyncWithDb();
            return UnavailableParts;
        }

        /// <summary>
        /// Retrieves all parts in the inventory.
        /// </summary>
        /// <returns></returns>
        public static List<PartQuantity> GetAllParts()
        {
            SyncWithDb();
            return AllParts;
        }

        /// <summary>
        /// Method that loops through all parts and updates the quantity of the
        /// provided <paramref name="part"/>.
        /// </summary>
        /// <param name="part">The part that needs to be restocked.</param>
        /// <param name="quantity">The desired quantity to be purchased.</param>
        public static void PurchaseParts(CarPart part, int quantity)
        {
            foreach (PartQuantity pq in GetAllParts())
            {
                if (pq.PartId == part.GetId())
                {
                    pq.Restock(quantity);
                    break;
                }
            }
        }

        /// <summary>
        /// Inspects all parts from the model then purchases each one of them that is not available.
        /// </summary>
        /// <param name="m">Model that should be processed.</param>
        public static void VerifyQuantityForModel(CarModel m)
        {
            SyncWithDb();
            foreach (CarPart part in m.GetAllParts())
            {
                if (ForRestock(part))
                {
                    PurchaseParts(part, 10);
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Method that checks if a part is in stock and determines if it needs to be purchased.
        /// </summary>
        /// <param name="cp"> The part that is being counted.</param>
        /// <returns></returns>
        private static bool ForRestock(CarPart cp)
        {
            foreach (PartQuantity pq in UnavailableParts)
            {
                if (pq.PartId == cp.GetId())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update the list of available parts with realtime data from the DB.
        /// </summary>
        private static void SyncWithDb()
        {
            AvailableParts = new List<PartQuantity>();
            UnavailableParts = new List<PartQuantity>();
            AllParts = new List<PartQuantity>();

            // Open connection to database
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {


                con.Open();

                // Get part quantity from DB
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT `part_id`, `quantity`, `name`, `type` FROM `inventory` WHERE `quantity` > 0", con);
                MySqlDataReader reader = cmd.ExecuteReader();
                PartQuantity pq;

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader[0]);
                    int quantity = Convert.ToInt32(reader[1]);
                    string name = reader[2].ToString();
                    string type = reader[3].ToString();

                    pq = new PartQuantity(id, quantity, name, type);
                    AvailableParts.Add(pq);
                    AllParts.Add(pq);
                }
                reader.Close();

                cmd = new MySqlCommand("SELECT `part_id`, `quantity`, `name`, `type` FROM `inventory` WHERE `quantity` = 0", con);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader[0]);
                    int quantity = Convert.ToInt32(reader[1]);
                    string name = reader[2].ToString();
                    string type = reader[3].ToString();

                    pq = new PartQuantity(id, quantity, name, type);
                    UnavailableParts.Add(pq);
                    AllParts.Add(pq);
                }
                reader.Close();

                // Close conenction
                con.Close();
            }
        }
        #endregion

        #endregion
    }
}