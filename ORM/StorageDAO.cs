using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class StorageDAO
    {
        public static String TableName = "Storage";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_ID = SQL_SELECT +  " WHERE storage_id=@storage_id";
        public static String SQL_INSERT = "Insert into " + TableName + " (storage_name, address_id) values (@storage_name," +
                                          " @address_id)";
        
        public static Storage SelectById(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@storage_id",id);
            SqlDataReader reader = db.Select(command);
            Storage result = Read(reader)[0];
            db.Close();
            return result;
        }
        
        public static Collection<Storage> Read(SqlDataReader reader)
        {
            Collection<Storage> result = new Collection<Storage>();
            while (reader.Read())
            {
                Storage storage = new Storage();

                storage.storage_id = Convert.ToInt32(reader["storage_id"]);
                storage.storage_name = Convert.ToString(reader["storage_name"]);

                int address_id = Convert.ToInt32(reader["address_id"]);
                storage.address = AddressDAO.SelectById(address_id);
                
                result.Add(storage);
            }
            return result;
        }

        public static Collection<Storage> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Storage> storage = Read(reader);
            db.Close();
            return storage;
        }
        
        public static int Insert(Storage storage)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@storage_name", storage.storage_name);
            command.Parameters.AddWithValue("@address_id", storage.address.address_id);
            
            int ret = db.ExecuteNonQuery(command);
            
            SqlCommand command2 = db.CreateCommand("SELECT IDENT_CURRENT('Storage')");
            int id = Convert.ToInt32(command2.ExecuteScalar());
            storage.storage_id = id;

            return id;
        }
    }
}