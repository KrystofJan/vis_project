using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class StockDAO
    {
        public static String TableName = "Stock";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_BY_MOVIE_ID = SQL_SELECT +  " WHERE movie_id=@movie_id";
        public static String SQL_SELECT_BY_Storage_ID = SQL_SELECT +  " WHERE storage_id=@storage_id";
        public static String SQL_INSERT = "Insert into " + TableName + " (movie_id, storage_id, ammount) values (@movie_id," +
                                          " @storage_id, @ammount)";
        
        public static Collection<Stock> SelectByMovieId(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_MOVIE_ID);
            command.Parameters.AddWithValue("@movie_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<Stock> result = Read(reader);
            db.Close();
            return result;
        }
        public static Collection<Stock> SelectByStorageId(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_Storage_ID);
            command.Parameters.AddWithValue("@storage",id);
            SqlDataReader reader = db.Select(command);
            Collection<Stock> result = Read(reader);
            db.Close();
            return result;
        }
        public static Collection<Stock> Read(SqlDataReader reader)
        {
            Collection<Stock> result = new Collection<Stock>();
            while (reader.Read())
            {
                Stock stock = new Stock();

                int movie_id = Convert.ToInt32(reader["movie_id"]);
                int storage_id = Convert.ToInt32(reader["storage_id"]);
                stock.movie = MovieDAO.SelectById(movie_id);
                stock.storage = StorageDAO.SelectById(storage_id);
                stock.ammount = Convert.ToInt32(reader["ammount"]);
                
                result.Add(stock);
            }
            return result;
        }

        public static Collection<Stock> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Stock> storage = Read(reader);
            db.Close();
            return storage;
        }
        
        public static void Insert(Stock stock)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@storage_id", stock.storage.storage_id);
            command.Parameters.AddWithValue("@movie_id", stock.movie.movie_id);
            command.Parameters.AddWithValue("@ammount", stock.ammount);

            db.ExecuteNonQuery(command);
        }
    }
}