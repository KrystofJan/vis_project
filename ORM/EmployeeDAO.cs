using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class EmployeeDAO
    {
        public static String TableName = "Employee";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_ID = SQL_SELECT + " WHERE employee_id=@employee_id";

        public static String SQL_INSERT = "AddEmployee";
        
        public static String Insert(Employee employee)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            command.CommandType = CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@first_name", employee.first_name);
            command.Parameters.AddWithValue("@last_name", employee.last_name);
            command.Parameters.AddWithValue("@address_id", employee.address.address_id);
            command.Parameters.AddWithValue("@email", employee.email);
            command.Parameters.AddWithValue("@phone", employee.phone);
            command.Parameters.AddWithValue("@salary", employee.salary);
            command.Parameters.AddWithValue("@storage_id", employee.storage.storage_id);
            
            command.ExecuteNonQuery();
            
            SqlCommand command3 = db.CreateCommand("SELECT IDENT_CURRENT('person_detail')");
            int pdid = Convert.ToInt32(command3.ExecuteScalar());
            PersonDetail pd = PersonDetailDAO.SelectById(pdid);
            
            employee.person_detail = pd;
            SqlCommand command2 = db.CreateCommand("SELECT employee_id from employee where person_detail_id=@person_detail_id");
            command2.Parameters.AddWithValue("@person_detail_id", pdid);
            string id = Convert.ToString(command2.ExecuteScalar());
            employee.employee_id = id;
            
            db.Close();
            return id;
        }
        public static Employee SelectById(string id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@employee_id",id);
            SqlDataReader reader = db.Select(command);
            Employee result = Read(reader)[0];
            db.Close();
            return result;
        }
        
        public static Collection<Employee> Read(SqlDataReader reader)
        {
            Collection<Employee> result = new Collection<Employee>();
            while (reader.Read())
            {
                Employee employee = new Employee();
                int storage_id = Convert.ToInt32(reader["storage_id"]);
                int person_detail_id = Convert.ToInt32(reader["person_detail_id"]);

                employee.employee_id = Convert.ToString(reader["employee_id"]);
                employee.salary =  Convert.ToDecimal(reader["salary"]);
                employee.storage = StorageDAO.SelectById(storage_id);
                // TODO: Change logic so that I can find person detail data
                employee.person_detail = PersonDetailDAO.SelectById(person_detail_id);

                result.Add(employee);
            }
            return result;
        }
    }
}