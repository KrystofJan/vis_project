using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class AddressDAO
    {
        public static String TableName = "Address";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_ID = SQL_SELECT + " WHERE address_id=@address_id";

        public static String SQL_INSERT = "Insert into " + TableName +
                                          " (country, city, street, building_number) values (@country, @city, @street," +
                                          " @building_number)";
        
        public static Address SelectById(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            // TODO READER and make generic
            command.Parameters.AddWithValue("@address_id",id);
            SqlDataReader reader = db.Select(command);
            Address result = Read(reader)[0];
            db.Close();
            return result;

        }
        public static Collection<Address> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<Address> result = Read(reader);
            db.Close();
            return result;

        }
        public static Collection<Address> Read(SqlDataReader reader)
        {
            Collection<Address> result = new Collection<Address>();
            while (reader.Read())
            {
                Address address = new Address();
                address.address_id = Convert.ToInt32(reader["address_id"]);
                address.country = Convert.ToString(reader["country"]);
                address.city = Convert.ToString(reader["city"]);
                address.street = Convert.ToString(reader["street"]);
                address.building_number = Convert.ToString(reader["building_number"]);
        
                result.Add(address);
            }

            return result;
        }

        public static int Insert(Address address)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@country", address.country);
            command.Parameters.AddWithValue("@city", address.city);
            command.Parameters.AddWithValue("@street", address.street);
            command.Parameters.AddWithValue("@building_number", address.building_number);
            
            int ret = db.ExecuteNonQuery(command);
            
            SqlCommand command2 = db.CreateCommand("SELECT IDENT_CURRENT('Address')");
            int id = Convert.ToInt32(command2.ExecuteScalar());
            address.address_id = id;
            db.Close();
            return id;
        }
    }
}