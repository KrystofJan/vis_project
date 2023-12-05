using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    
    public class PersonDetailDAO
    {
        private static Database db = Database.Instance;
        public static String TableName = "person_detail";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_ID = SQL_SELECT + " WHERE person_detail_id=@person_detail_id";
    
        public static PersonDetail SelectById(int id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@person_detail_id",id);
            SqlDataReader reader = db.Select(command);
            PersonDetail result = Read(reader)[0];
            db.Close();
            reader.Close();
            return result;
        }
    
        public static Collection<PersonDetail> Read(SqlDataReader reader)
        {
            Collection<PersonDetail> result = new Collection<PersonDetail>();
            while (reader.Read())
            {
                PersonDetail person = new PersonDetail();

                person.person_detail_id = Convert.ToInt32(reader["person_detail_id"]);
                person.created_date = Convert.ToDateTime(reader["created_date"]);
                person.updated_date = Convert.ToDateTime(reader["updated_data"]);
                person.email = Convert.ToString(reader["email"]);
                person.first_name = Convert.ToString(reader["first_name"]);
                person.last_name = Convert.ToString(reader["last_name"]);
                person.is_active = Convert.ToBoolean(reader["is_active"]);
                person.phone = Convert.ToString(reader["phone"]);
                int addr_id = Convert.ToInt32(reader["address_id"]);
                person.address = AddressDAO.SelectById(addr_id);
            
                result.Add(person);
            }
            return result;
        }

    }
}