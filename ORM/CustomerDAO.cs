using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class CustomerDAO
    {
        public static String TableName = "Customer";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_ID = SQL_SELECT + " WHERE customer_id=@customer_id";

        public static String SQL_INSERT = "AddCustomer";
        
        public static String Insert(Customer customer)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            command.CommandType = CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@first_name", customer.person_detail.first_name);
            command.Parameters.AddWithValue("@last_name", customer.person_detail.last_name);
            command.Parameters.AddWithValue("@address_id", customer.person_detail.address.address_id);
            command.Parameters.AddWithValue("@email", customer.person_detail.email);
            command.Parameters.AddWithValue("@phone", customer.person_detail.phone);
            
            command.ExecuteNonQuery();

            SqlCommand command3 = db.CreateCommand("SELECT IDENT_CURRENT('person_detail')");
            int pdid = Convert.ToInt32(command3.ExecuteScalar());
            PersonDetail pd = PersonDetailDAO.SelectById(pdid);
            
            customer.person_detail = pd;
            SqlCommand command2 = db.CreateCommand("SELECT customer_id from customer where person_detail_id=@person_detail_id");
            command2.Parameters.AddWithValue("@person_detail_id", pdid);
            string id = Convert.ToString(command2.ExecuteScalar());
            customer.customer_id = id;
            
            db.Close();
            return id;
        }
        public static Customer SelectById(string id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@customer_id",id);
            SqlDataReader reader = db.Select(command);
            Customer result = Read(reader)[0];
            db.Close();
            return result;
        }
        
        public static Collection<Customer> Read(SqlDataReader reader)
        {
            Collection<Customer> result = new Collection<Customer>();
            while (reader.Read())
            {
                Customer customer = new Customer();
                int person_detail_id = Convert.ToInt32(reader["person_detail_id"]);

                customer.customer_id = Convert.ToString(reader["customer_id"]);
                customer.person_detail = PersonDetailDAO.SelectById(person_detail_id);
                customer.total_rentals = Convert.ToInt32(reader["total_rentals"]);

                result.Add(customer);
            }
            return result;
        }
    }
}