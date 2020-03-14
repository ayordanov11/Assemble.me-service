using Assemble.me.Library.Parts;
using Assemble.me.Library.Parts.PackageAccumulator;
using Assemble.me.Library.Parts.PackageChassis;
using Assemble.me.Library.Parts.PackageEngine;
using Assemble.me.Library.Parts.PackageExtra;
using Assemble.me.Library.Parts.PackageRims;
using Assemble.me.Library.Parts.PackageSuspension;
using Assemble.me.Library.Parts.PackageTires;
using Assemble.me.Library.Parts.PackageTransmission;
using Assemble.me.Library.Parts.PackageInterrior;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Linq;

namespace Assemble.me.Library
{
    /// <summary>
    /// A class describing a car model.
    /// </summary>
    public class CarModel
    {
        #region Properties
        public int ID { get; set; }
        public string Name { get; private set; }
        public Engine Engine { get; set; }
        public Accumulator Accumulator { get; set; }
        public Suspension Suspension { get; set; }
        public Chassis Chassis { get; set; }
        public Transmission Transmission { get; set; }
        public Interior Interior { get; set; }
        public Tires Tires { get; set; }
        public Rims Rims { get; set; }
        public List<Extra> Extras { get; set; }

        // List of all parts used in The inventory class to keep track of the quantity in stock.
        private List<CarPart> Parts = new List<CarPart>();
        #endregion

        #region Constructors
        public CarModel()
        {
            this.Extras = new List<Extra>();
            this.Name = "N/A"; // default value to avoid null reference expections
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method that assigns a <see cref="CarPart"/> to the model.
        /// </summary>
        /// <param name="part">The part that should be added to the model.</param>
        public void AddPart(CarPart part)
        {
            part.Add(this);
            this.Parts.Add(part);
        }

        /// <summary>
        /// Method that removes a <see cref="CarPart"/> from the model.
        /// </summary>
        /// <param name="part">he part that should be removed from the model.</param>
        public void RemovePart(CarPart part)
        {
            part.Remove(this);
            this.Parts.RemoveAll(x => x.GetType().Name == part.GetType().Name);
        }

        /// <summary>
        /// Saves the car model to the database and retrieves its unique id.
        /// </summary>
        public void SaveToDB()
        {
            // Get and open the connection
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {


                con.Open();

                // Insert the model's properties to the database
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO `car_model`(`accumulator_id`, `tires_id`, `rims_id`, `interior_id`, `engine_id`, " +
                    "`suspension_id`, `transmission_id`, `chassis_id`, `extra_id`, `name`) " +
                    "VALUES (@acc_id,@t_id,@r_id,@i_id,@e_id,@s_id,@trans_id,@c_id,@ex_id,@name)", con);
                cmd.Parameters.AddWithValue("@acc_id", this.Accumulator.GetId());
                cmd.Parameters.AddWithValue("@t_id", this.Tires.GetId());
                cmd.Parameters.AddWithValue("@r_id", this.Rims.GetId());
                cmd.Parameters.AddWithValue("@i_id", this.Interior.GetId());
                cmd.Parameters.AddWithValue("@e_id", this.Engine.GetId());
                cmd.Parameters.AddWithValue("@s_id", this.Suspension.GetId());
                cmd.Parameters.AddWithValue("@trans_id", this.Transmission.GetId());
                cmd.Parameters.AddWithValue("@c_id", this.Chassis.GetId());
                cmd.Parameters.AddWithValue("@ex_id", this.GetExtrasIds());
                cmd.Parameters.AddWithValue("@name", this.Name);

                cmd.ExecuteNonQuery();

                // Retrieve the id from the database and assign it to the object
                this.ID = Convert.ToInt32(cmd.LastInsertedId);

                // Close the connection
                con.Close();
            }
        }

        /// <summary>
        /// Sets the name of the model.
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets a list of all parts in the model. 
        /// </summary>
        /// <returns>A list of parts.</returns>
        public List<CarPart> GetAllParts()
        {
            return Parts;
        }

        /// <summary>
        /// Calculates the total price of the model.
        /// </summary>
        /// <returns>A sum of all the prices of the parts in the model.</returns>
        public decimal GetPrice()
        {
            decimal total = 0;
            foreach (CarPart part in Parts)
            {
                total += part.Price;
            }

            return total;
        }

        /// <summary>
        /// Calculates the current production time of the model in days.
        /// </summary>
        /// <returns>A sum of the rpoduction times of all parts.</returns>
        public int GetProductionTime()
        {
            int total = 0;
            foreach(CarPart part in Parts)
            {
                total += part.ProductionTime;
            }
            return total;
        }
 
        #region Helpers

        /// <summary>
        /// Converts database information into a Car model object.
        /// </summary>
        /// <param name="modelId"> The model id of the CarModel, that should be recreated.</param>
        /// <returns>A car model that should be recreated by id.</returns>
        public static CarModel RecreateModelById(int modelId)
        {
            // A car model that will become the recreated from the database CarModel object.
            CarModel model = new CarModel();

            // Open connection to database
            MySqlConnection con = ApplicationSettings.GetConnection();
            con.Open();

            // Get model information from DB
            MySqlCommand cmd = new MySqlCommand(
                "SELECT * FROM `car_model` WHERE `model_id` = @model", con);
            cmd.Parameters.AddWithValue("@model", modelId);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 1; i < 10; i++)
                {
                    // Extras
                    if (i == 8)
                    {
                        string s = reader[i].ToString();
                        string[] extras = s.Split(null);
                        foreach (string e in extras)
                        {
                            model.AddPart(ApplicationSettings.GetPartById(Convert.ToInt32(e)));
                        }
                    }
                    // Name
                    else if (i == 9)
                    {
                        if (reader[10] != DBNull.Value)
                            model.SetName(reader[10].ToString());
                    }
                    else
                    {
                        int id = Convert.ToInt32(reader[i]);

                        // Insert the found by id part into the model
                        model.AddPart(ApplicationSettings.GetPartById(id));
                    }  
                }
            }
            reader.Close();

            // Close conenction
            con.Close();

            return model;
        }

        /// <summary>
        /// Gets all the unique identifiers and returns them as a string separating the identifiers with a white space. This
        /// is needed in order to store the model to the database.
        /// </summary>
        /// <returns>A string with the identifiers of all extras separated by white space.</returns>
        private string GetExtrasIds()
        {
            string ids = "";
            foreach (Extra e in Extras)
            {
                ids+=e.GetId().ToString() + " ";
            }
            return ids;
        }

        #endregion

        #endregion
    }
}
