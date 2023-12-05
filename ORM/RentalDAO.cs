using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class RentalDAO
    {
        private static Database db = Database.Instance;
        public static String TableName = "Rental";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_ID = SQL_SELECT +  " WHERE rental_id=@rental_id";
        public static String SQL_INSERT = "Insert into " + TableName + " (date_of_start, date_of_return, customer_id, employee_id) values (@date_of_start," +
                                          " @date_of_return, @customer_id, @employee_id)";
        
        public static Rental SelectById(int id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@rental_id",id);
            SqlDataReader reader = db.Select(command);
            Rental result = Read(reader)[0];
            db.Close();
            reader.Close();
            return result;
        }
        
        public static Collection<Rental> Read(SqlDataReader reader)
        {
            Collection<Rental> result = new Collection<Rental>();
            while (reader.Read())
            {
                Rental rental = new Rental();

                rental.rental_id = Convert.ToInt32(reader["rental_id"]);
                string customer_id = Convert.ToString(reader["customer_id"]);
                rental.customer = CustomerDAO.SelectById(customer_id);
                string employee_id = Convert.ToString(reader["employee_id"]);
                rental.employee = EmployeeDAO.SelectById(employee_id);
                rental.date_of_return = Convert.ToDateTime(reader["date_of_return"]);
                rental.date_of_start = Convert.ToDateTime(reader["date_of_start"]);
                rental.is_returned = Convert.ToBoolean(reader["is_returned"]);

                rental.discs = DiscDAO.SelectByRentalId(rental.rental_id);
                
                result.Add(rental);
            }
            return result;
        }

        public static Collection<Rental> Select()
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Rental> storage = Read(reader);
            db.Close();
            reader.Close();
            return storage;
        }
        
        public static int Insert(Rental rental)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@customer_id", rental.customer.customer_id);
            command.Parameters.AddWithValue("@date_of_return", rental.date_of_return);
            command.Parameters.AddWithValue("@date_of_start", rental.date_of_start);
            command.Parameters.AddWithValue("@employee_id", rental.employee.employee_id);
            
            int ret = db.ExecuteNonQuery(command);
            
            SqlCommand command2 = db.CreateCommand("SELECT IDENT_CURRENT('Rental')");
            int id = Convert.ToInt32(command2.ExecuteScalar());
            rental.rental_id = id;

            return id;
        }
    }
}