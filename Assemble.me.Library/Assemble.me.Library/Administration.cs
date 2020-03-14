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
    public class Administration
    {
        #region Properties
        public List<Order> Orders { get; set; }
        private List<Order> ProcessedOrders { get; set; }
        public List<Customer> Customers { get; set; }
        public Randomization Randomization { get; set; }
        #endregion

        #region Constructor
        public Administration()
        {
            Orders = new List<Order>();
            ProcessedOrders = new List<Order>();
            Customers = new List<Customer>();
            Randomization = new Randomization();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method that provides a list of all existing orders currently accepted for processing.
        /// </summary>
        /// <returns>List of Orders ready for processing.</returns>
        public List<Order> GetOrders()
        {
            this.SyncOrders();
            return Orders;
        }

        /// <summary>
        /// Provides a list of all processed orders.
        /// </summary>
        /// <returns>List of orders which have already been processed.</returns>
        public List<Order> GetProcessedOrders()
        {
            this.SyncProcessedOrders();
            return this.ProcessedOrders;
        }

        /// <summary>
        /// Finds an order in the list of orders.
        /// </summary>
        /// <param name="orderId">The unqiue identifier of the order that is searched for.</param>
        /// <returns>The order with the specified <paramref name="orderId"/> or null.</returns>
        public Order FindOrderById(int orderId)
        {
            foreach (Order o in this.GetOrders())
            {
                if (o.ID == orderId)
                    return o;
            }
            return null;
        }

        /// <summary>
        /// Finds an order, restocks the required items and parts, removes it from the queue and saves it in the order history list.
        /// </summary>
        /// <param name="orderId"></param>
        public void ProcessOrder(int orderId)
        {
            Order o = FindOrderById(orderId);
            Inventory.VerifyQuantityForModel(o.Model);
            o.ProcessOrder();

            // Open connection to database
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {


                con.Open();

                // Insert order's properties to database
                MySqlCommand cmd = new MySqlCommand("UPDATE `order` SET `processed_date`= @processed_date where `order_id`= @id;", con);
                cmd.Parameters.AddWithValue("@processed_date", o.ProcessedAt);
                cmd.Parameters.AddWithValue("@id", orderId);

                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("REPLACE INTO `processed_order`(`processed_order_id`, `model_id`, `customer_id`, `issued_date`," +
                    " `expected_date`, `processed_date`, `priority`)" +
                    " VALUES (@id,@model_id,@cust_id,@issued_d,@excp_date,@processed_date,@prio)", con);

                cmd.Parameters.AddWithValue("@id", o.ID);
                cmd.Parameters.AddWithValue("@model_id", o.Model.ID);
                cmd.Parameters.AddWithValue("@cust_id", o.Customer.ID);
                cmd.Parameters.AddWithValue("@issued_d", o.CreatedAt);
                cmd.Parameters.AddWithValue("@excp_date", o.ExpectedDate);
                cmd.Parameters.AddWithValue("@processed_date", o.ProcessedAt);
                cmd.Parameters.AddWithValue("@prio", o.Priority);

                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("DELETE FROM `order` WHERE `order_id`=@id", con);
                cmd.Parameters.AddWithValue("@id", o.ID);

                cmd.ExecuteNonQuery();

                // Close connection
                con.Close();
            }
        }

        #region Database Communication Methods

        /// <summary>
        /// Deletes all unprocessed orders from the database.
        /// </summary>
        public void ClearDatabaseUnprocessedOrders()
        {
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {
                con.Open();

                // Deletes all unprocessed orders from the database. Where 1=1 is placed to prevent possible safe-update checks
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `order` WHERE 1=1;", con);
                cmd.ExecuteNonQuery();

                // Close connection
                con.Close();
            }
        }

        /// <summary>
        /// Deletes all processed orders from the database.
        /// </summary>
        public void ClearDatabaseProcessedOrders()
        {
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {
                con.Open();

                // Deletes all processed orders from the database. Where 1=1 is placed to prevent possible safe-update checks
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `processed_order` WHERE 1=1;", con);
                cmd.ExecuteNonQuery();

                // Close connection
                con.Close();
            }
        }

        /// <summary>
        /// Synchronizes the order list with the database information.
        /// </summary>
        private void SyncOrders()
        {
            this.Orders = new List<Order>();

            // Open connection to database
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {


                con.Open();

                // Get all orders, customers and models from DB
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `order` AS O JOIN `car_model` " +
                    "AS M on (O.model_id=M.model_id) JOIN `customer` AS C on (O.customer_id = C.customer_id)", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int order_id = Convert.ToInt32(reader[0]);
                    int model_id = Convert.ToInt32(reader[1]);
                    int customer_id = Convert.ToInt32(reader[2]);
                    DateTime issued_date = Convert.ToDateTime(reader[3]);
                    DateTime expected_date = Convert.ToDateTime(reader[4]);
                    int priority = Convert.ToInt32(reader[6]);
                    List<int> partIds = new List<int>();
                    partIds.Add(Convert.ToInt32(reader[8]));
                    partIds.Add(Convert.ToInt32(reader[9]));
                    partIds.Add(Convert.ToInt32(reader[10]));
                    partIds.Add(Convert.ToInt32(reader[11]));
                    partIds.Add(Convert.ToInt32(reader[12]));
                    partIds.Add(Convert.ToInt32(reader[13]));
                    partIds.Add(Convert.ToInt32(reader[14]));
                    partIds.Add(Convert.ToInt32(reader[15]));
                    string extras = reader[16].ToString();
                    // TODO name reader[17]
                    string fname = reader[19].ToString();
                    string lname = reader[20].ToString();
                    string email = reader[21].ToString();
                    string phone = reader["phone"].ToString();

                    foreach (string extra in extras.Split(null))
                    {
                        if (!(extra == ""))
                            partIds.Add(Convert.ToInt32(extra));
                    }

                    // Create model
                    CarModel model = new CarModel();
                    foreach (int id in partIds)
                    {
                        model.AddPart(ApplicationSettings.GetPartById(id));
                    }

                    model.ID = model_id;

                    // Create customer
                    Customer cust = new Customer(fname, lname, email, phone);
                    cust.ID = customer_id;

                    // Create order
                    Order o = new Order(cust, model, RecreateOrderPriorityFromDB(priority));
                    o.ID = order_id;
                    o.SetExpectedDate(expected_date);
                    o.SetTimeOfCreation(issued_date);

                    this.Orders.Add(o);
                }

                // Close connection
                con.Close();
            }

        }

        /// <summary>
        /// Synchronizes the already processed orders list with the information in the database.
        /// </summary>
        private void SyncProcessedOrders()
        {
            this.ProcessedOrders = new List<Order>();

            // Open connection to database
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {
                con.Open();

                // Get all orders, customers and models from DB
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `processed_order` AS O JOIN `car_model` " +
                    "AS M on (O.model_id=M.model_id) JOIN `customer` AS C on (O.customer_id = C.customer_id)", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int order_id = Convert.ToInt32(reader[0]);
                    int model_id = Convert.ToInt32(reader[1]);
                    int customer_id = Convert.ToInt32(reader[2]);
                    DateTime issued_date = Convert.ToDateTime(reader[3]);
                    DateTime processed_date = Convert.ToDateTime(reader[5]);
                    DateTime expected_date = Convert.ToDateTime(reader[4]);
                    int priority = Convert.ToInt32(reader[6]);
                    List<int> partIds = new List<int>();
                    partIds.Add(Convert.ToInt32(reader[8]));
                    partIds.Add(Convert.ToInt32(reader[9]));
                    partIds.Add(Convert.ToInt32(reader[10]));
                    partIds.Add(Convert.ToInt32(reader[11]));
                    partIds.Add(Convert.ToInt32(reader[12]));
                    partIds.Add(Convert.ToInt32(reader[13]));
                    partIds.Add(Convert.ToInt32(reader[14]));
                    partIds.Add(Convert.ToInt32(reader[15]));
                    string extras = reader[16].ToString();
                    // TODO name reader[17]
                    string fname = reader[19].ToString();
                    string lname = reader[20].ToString();
                    string email = reader[21].ToString();
                    string phone = reader[22].ToString();

                    foreach (string extra in extras.Split(null))
                    {
                        if (!(extra == ""))
                            partIds.Add(Convert.ToInt32(extra));
                    }

                    // Create model
                    CarModel model = new CarModel();
                    foreach (int id in partIds)
                    {
                        model.AddPart(ApplicationSettings.GetPartById(id));
                    }

                    model.ID = model_id;

                    // Create customer
                    Customer cust = new Customer(fname, lname, email, phone);
                    cust.ID = customer_id;

                    // Create order
                    Order o = new Order(cust, model, RecreateOrderPriorityFromDB(priority));
                    o.ID = order_id;
                    o.SetExpectedDate(expected_date);
                    o.SetTimeOfCreation(issued_date);
                    o.ProcessOrder(processed_date);

                    this.ProcessedOrders.Add(o);
                }

                // Close connection
                con.Close();
            }
        }

        /// <summary>
        /// Converts database information into a OrderPriority object
        /// </summary>
        /// <param name="priority"> The integer value 0 or 1 that represents the priprity of an order.</param>
        /// <returns> A priority object determinde by a value 0 or 1</returns>
        private OrderPriority RecreateOrderPriorityFromDB(int priority = 0)
        {
            if (priority == 1)
                return OrderPriority.High;

            return OrderPriority.Normal;
        }


        #endregion

        #endregion
    }
}
