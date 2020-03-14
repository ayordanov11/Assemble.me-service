using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.PackageCustomer
{
    public class Customer
    {

        #region Properties
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        #endregion

        #region Constructor
        public Customer(string firstName,string lastName, string email,string phone){          
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves the customer to the database
        /// </summary>
        public void SaveToDB()
        {
            CustomerClient temp = new CustomerClient();
            if (temp.CheckIfEmailIsTaken(Email))
            {
                // Get and open the connection
                using (MySqlConnection con = ApplicationSettings.GetConnection())
                {
                    con.Open();

                    // Insert the customer's properties to the database
                    MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO `customer`(`first_name`, `last_name`, `email`, `phone`) "
                        + "VALUES (@fname,@lname,@email,@phone)", con);
                    cmd.Parameters.AddWithValue("@fname", this.FirstName);
                    cmd.Parameters.AddWithValue("@lname", this.LastName);
                    cmd.Parameters.AddWithValue("@email", this.Email);
                    cmd.Parameters.AddWithValue("@phone", this.Phone);
                    cmd.ExecuteNonQuery();

                    // Retrieve the id from the database and assign it to the object
                    this.ID = Convert.ToInt32(cmd.LastInsertedId);

                    // Close the connection
                    con.Close();
                }
            }
            
        }

        /// <summary>
        /// Converts database information into a Customer object
        /// </summary>
        /// <param name="customerId"> The id of the customer that should be recreated.</param>
        /// <returns>A customer that should be recreated by id.</returns>
        public static Customer RecreateCustomerById(int customerId)
        {
            // A customer that will become the recreated from the database Customer object.
            Customer temp = null;

            // Open connection to database
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {
                con.Open();

                // Get model information from DB
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT `customer_id`, `first_name`, `last_name`, `email`, `phone` FROM `customer` WHERE `customer_id` = @customer", con);
                cmd.Parameters.AddWithValue("@customer", customerId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int cId = Convert.ToInt32(reader[0]);
                    string cName = Convert.ToString(reader[1]);
                    string cLastName = Convert.ToString(reader[2]);
                    string cEmail = Convert.ToString(reader[3]);
                    string cPhone = Convert.ToString(reader[4]);

                    temp = new Customer(cName, cLastName, cEmail, cPhone);

                }
                reader.Close();

                // Close conenction
                con.Close();
            }

            return temp;
        }

        /// <summary>
        /// Retrieves the full name of the customer.
        /// </summary>
        /// <returns>A fromatted string with the first and last name of the customer.</returns>
        public string GetName()
        {
            return this.FirstName + " " + this.LastName;
        }
        #endregion
    }
}
