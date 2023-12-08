using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class CustomerDAO
    {
        private static Database db = Database.Instance;
        public static String TableName = "Customer";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;

        public static String SQL_SELECT_ALL =
            "select person_detail.*, customer.customer_id, customer.total_rentals from customer inner join person_detail on customer.person_detail_id = person_detail.person_detail_id";
        public static String SQL_SELECT_ID = SQL_SELECT + " WHERE customer_id=@customer_id";

        public static String SQL_INSERT = "AddCustomer";
        
        public static String Insert(Customer customer)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            command.CommandType = CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@first_name", customer.first_name);
            command.Parameters.AddWithValue("@last_name", customer.last_name);
            command.Parameters.AddWithValue("@address_id", customer.address.address_id);
            command.Parameters.AddWithValue("@email", customer.email);
            command.Parameters.AddWithValue("@phone", customer.phone);
            
            command.ExecuteNonQuery();

            SqlCommand command3 = db.CreateCommand("SELECT IDENT_CURRENT('person_detail')");
            int pdid = Convert.ToInt32(command3.ExecuteScalar());
            PersonDetail pd = PersonDetailDAO.SelectById(pdid);
            
            customer.person_detail_id = pd.person_detail_id;
            customer.created_date = pd.created_date;
            customer.updated_date = pd.updated_date;
            customer.is_active = pd.is_active;
            customer.total_rentals = 0;
            
            SqlCommand command2 = db.CreateCommand("SELECT customer_id from customer where person_detail_id=@person_detail_id");
            command2.Parameters.AddWithValue("@person_detail_id", pdid);
            string id = Convert.ToString(command2.ExecuteScalar());
            customer.customer_id = id;
            
            db.Close();
            return id;
        }
        public static Customer SelectById(string id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@customer_id",id);
            SqlDataReader reader = db.Select(command);
            Customer result = Read(reader)[0];
            reader.Close();
            db.Close();
            return result;
        }
        
        public static Collection<Customer> Select()
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);
            Collection<Customer> result = Read(reader);
            reader.Close();
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
                customer.total_rentals = Convert.ToInt32(reader["total_rentals"]);
                
               customer.person_detail_id = Convert.ToInt32(reader["person_detail_id"]);
               customer.created_date = Convert.ToDateTime(reader["created_date"]);
               customer.updated_date = Convert.ToDateTime(reader["updated_data"]);
               customer.email = Convert.ToString(reader["email"]);
               customer.first_name = Convert.ToString(reader["first_name"]);
               customer.last_name = Convert.ToString(reader["last_name"]);
               customer.is_active = Convert.ToBoolean(reader["is_active"]);
               customer.phone = Convert.ToString(reader["phone"]);

                result.Add(customer);
            }
            return result;
        }
    }
}