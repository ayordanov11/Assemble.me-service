using Assemble.me.Library.PackageCustomer;
using Assemble.me.Library.Parts;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.PackageOrder
{
    public class Order
    {
        #region Properties
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public CarModel Model { get; set; }
        public OrderPriority Priority { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ProcessedAt { get; private set; }
        public DateTime ExpectedDate { get; private set; }
        #endregion

        #region Constructors
        public Order(Customer c, CarModel m, OrderPriority priority = OrderPriority.Normal)
        {
            this.Customer = c;
            this.Model = m;
            // Orders have a Normal priority by default.
            this.ChangeOrderPriority(priority);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Changes the priority of the current order.
        /// </summary>
        /// <param name="priority">The new priority that should be assigned.</param>
        public void ChangeOrderPriority(OrderPriority priority)
        {
            this.Priority = priority;

            // Open connection to database
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {


                con.Open();

                // Change order's priority in database
                MySqlCommand cmd = new MySqlCommand("UPDATE `order` SET `priority`= @p where `order_id`= @id;", con);
                cmd.Parameters.AddWithValue("@p", this.Priority);
                cmd.Parameters.AddWithValue("@id", this.ID);

                cmd.ExecuteNonQuery();

                // Close connection
                con.Close();
            }
        }

        /// <summary>
        /// Processes the current order.
        /// </summary>
        public void ProcessOrder()
        {
            this.ProcessedAt = DateTime.Now;
        }

        /// <summary>
        /// Processes the current order and sets the time processed to the given 
        /// parameter.
        /// </summary>
        public void ProcessOrder(DateTime time)
        {
            this.ProcessedAt = time;
        }

        /// <summary>
        /// Sets the time that the order is being accepted for processing.
        /// </summary>
        public void SetTimeOfCreation()
        {
            this.CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Sets the time that the order is being accepted for processing to
        /// the provided parameter.
        /// </summary>
        public void SetTimeOfCreation(DateTime time)
        {
            this.CreatedAt = time;
        }

        /// <summary>
        /// Sets the expected date to the provied parameter. Used when
        /// recreating orders.
        /// </summary>
        /// <param name="time">The time an order is expected to be processed at.</param>
        public void SetExpectedDate(DateTime time)
        {
            this.ExpectedDate = time;
        }

        /// <summary>
        /// Calculates the estimated time to process the order, depending on priority.
        /// </summary>
        /// <returns>The expected delivery date for the orer.</returns>
        public DateTime CalculateEstimatedDeliveryDate()
        {
            int totalEstimatedTime = 0;
            foreach (CarPart cp in Model.GetAllParts())
            {
                totalEstimatedTime += cp.ProductionTime;
            }
            // If the order's priority is high the time is reduced.
            if (Priority == OrderPriority.High)
            {
                totalEstimatedTime = Convert.ToInt32(totalEstimatedTime * 0.75);
            }

            DateTime processed = this.CreatedAt;
            ExpectedDate = processed.AddDays(totalEstimatedTime);
            return this.ExpectedDate;
        }

        /// <summary>
        /// Calculates the new price for the model, if the priority is high.
        /// </summary>
        /// <returns>An increased price of the model.</returns>
        public decimal GetHighPriorityPrice()
        {
            return Model.GetPrice() * 1.25m;
        }

        /// <summary>
        /// Inserts the current order and all its properties to the database.
        /// </summary>
        public void InsertToDB()
        {
            this.SetTimeOfCreation();
            int custID = -1;
            // Save customer and car model to database
            this.Model.SaveToDB();
            // If user already exists
            if (!new CustomerClient().CheckIfEmailIsTaken(this.Customer.Email))
            {
                custID = new CustomerClient().GetCustomer(this.Customer.Email).ID;
            }
            else
            {
                this.Customer.SaveToDB();
                custID = this.Customer.ID;
            }
            // Open connection to database
            using (MySqlConnection con = ApplicationSettings.GetConnection())
            {
                con.Open();

                // Insert order's properties to database
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO `order`(`model_id`, `customer_id`, `issued_date`, `expected_date`, `processed_date`, `priority`) " +
                    "VALUES (@model, @customer, @issued_date, @expected_date, NULL, @priority)", con);
                cmd.Parameters.AddWithValue("@model", this.Model.ID);
                cmd.Parameters.AddWithValue("@customer", custID);
                cmd.Parameters.AddWithValue("@issued_date", this.CreatedAt);
                cmd.Parameters.AddWithValue("@expected_date", this.CalculateEstimatedDeliveryDate());
                cmd.Parameters.AddWithValue("@priority", this.Priority);

                cmd.ExecuteNonQuery();

                // Retrieve the id from the database and assign it to the object
                this.ID = Convert.ToInt32(cmd.LastInsertedId);

                con.Close();
            }
        }

        /// <summary>
        /// A short friendly string containing all major information about an order.
        /// </summary>
        /// <returns>A short info string.</returns>
        public override string ToString()
        {
            return "Order ID: " + this.ID + ", Model id: " + this.Model.ID + ", Created at: " + this.CreatedAt + ", Priority: " + this.Priority;
        }
        #endregion
    }

}
