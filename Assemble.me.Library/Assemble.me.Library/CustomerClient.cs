using Assemble.me.Library.PackageCustomer;
using Assemble.me.Library.PackageOrder;
using Assemble.me.Library.Parts;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library
{
    public class CustomerClient
    {
        #region Properties
        private CarModel CurrentModel { get;  set; }
        private Customer CurrentCustomer { get;  set; }
        #endregion

        #region Constructor
        public CustomerClient()
        {
            this.CurrentModel = new CarModel();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new model.
        /// </summary>
        public void NewModel()
        {
            this.CurrentModel = new CarModel();
        }

        /// <summary>
        /// Saves the current model to the database.
        /// </summary>
        public int SaveModel()
        {
            this.CurrentModel.SaveToDB();
            return this.CurrentModel.ID;
        }

        /// <summary>
        /// Load a model's reference into <see cref="CurrentModel"/>.
        /// </summary>
        /// <param name="model">The reference to the model.</param>
        public void LoadModel(CarModel model)
        {
            this.CurrentModel = model;
        }

        /// <summary>
        /// Creates and places an order with the specified <paramref name="customer"/> and
        /// <paramref name="model"/>. Optionally, there can be a priority, but by default it is <see cref="OrderPriority.Normal"/>.
        /// </summary>
        /// <param name="customer">The customer of the order.</param>
        /// <param name="model"></param>
        /// <param name="op"></param>
        public void OrderModel(Customer customer, CarModel model, OrderPriority op = OrderPriority.Normal)
        {
            this.PlaceOrder(new Order(customer, model, op));
        }

        /// <summary>
        /// Adds part to the current model.
        /// </summary>
        /// <param name="part">The car part to be added.</param>
        public void AddPartToModel(CarPart part)
        {
            this.CurrentModel.AddPart(part);
        }

        /// <summary>
        /// Removes part from the current model.
        /// </summary>
        /// <param name="part">The car part to be removed.</param>
        public void RemovePartFromModel(CarPart part)
        {
            this.CurrentModel.RemovePart(part);
        }

        /// <summary>
        /// Retrieves the current model.
        /// </summary>
        /// <returns></returns>
        public CarModel GetCurrentModel()
        {
            return this.CurrentModel;
        }

        /// <summary>
        /// Adds the order to the list of orders to be processed  and
        /// assigns the date and time that it was added to the queue.
        /// </summary>
        /// <param name="order"> The order that must be added in the processing queue.</param>
        private void PlaceOrder(Order order)
        {
            order.InsertToDB();
        }

        /// <summary>
        /// Sets the current customer with the provided parameter.
        /// </summary>
        /// <param name="customer">The new current customer.</param>
        public void SetCurrentCustomer(Customer customer)
        {
            this.CurrentCustomer = customer;
        }

        /// <summary>
        /// Retrieves the current customer.
        /// </summary>
        /// <returns>Returns the current customer.</returns>
        public Customer GetCurrentCustomer()
        {
            return this.CurrentCustomer;
        }

        /// <summary>
        /// Checks if the email exists in the database.
        /// </summary>
        /// <param name="email">The email to be checked.</param>
        /// <returns>True if email is free.</returns>
        public bool CheckIfEmailIsTaken(string email)
        {
            bool temp = false;
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {
                con.Open();

                // Get model information from DB
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT Count(*) FROM `customer` WHERE `email` = @email;", con);
                cmd.Parameters.AddWithValue("@email", email);
                MySqlDataReader reader = cmd.ExecuteReader();

                
                while (reader.Read())
                {
                    int count = Convert.ToInt32(reader[0]);
                    if (count == 0)
                    {
                        temp = true;
                    }
                }
                reader.Close();

                // Close conenction
                con.Close();
            }

            return temp;
        }

        /// <summary>
        /// Retrieves a customer with the specified <paramref name="customerId" /> from the database,
        /// if the customer does not exists null is returned.
        /// </summary>
        /// <param name="customerId">The unique identifier of the searched customer.</param>
        /// <returns>A customer object or null.</returns>
        public Customer GetCustomer(int customerId)
        {
            // A customer that will become the recreated from the database Customer object.
            Customer temp = null;

            // Open connection to database
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {
                con.Open();

                // Get model information from DB
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM `customer` WHERE `customer_id` = @id", con);
                cmd.Parameters.AddWithValue("@id", customerId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int cId = Convert.ToInt32(reader[0]);
                    string cName = Convert.ToString(reader[1]);
                    string cLastName = Convert.ToString(reader[2]);
                    string cEmail = Convert.ToString(reader[3]);
                    string cPhone = Convert.ToString(reader[4]);

                    temp = new Customer(cName, cLastName, cEmail, cPhone);
                    temp.ID = cId;
                }
                reader.Close();

                // Close conenction
                con.Close();
            }

            return temp;
        }

        /// <summary>
        /// Retrieves a customer by specified email.
        /// </summary>
        /// <param name="email">the customer's email.</param>
        /// <returns>A customer.</returns>
        public Customer GetCustomer(string email)
        {
            // A customer that will become the recreated from the database Customer object.
            Customer temp = null;

            // Open connection to database
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {
                con.Open();

                // Get model information from DB
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM `customer` WHERE `email` = @email", con);
                cmd.Parameters.AddWithValue("@email", email);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int cId = Convert.ToInt32(reader[0]);
                    string cName = Convert.ToString(reader[1]);
                    string cLastName = Convert.ToString(reader[2]);
                    string cEmail = Convert.ToString(reader[3]);
                    string cPhone = Convert.ToString(reader[4]);

                    temp = new Customer(cName, cLastName, cEmail, cPhone);
                    temp.ID = cId;
                }
                reader.Close();

                // Close conenction
                con.Close();
            }

            return temp;
        }
        #endregion
    }

}
