using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class StockDAO
    {
        private static Database db = Database.Instance;
        public static String TableName = "Stock";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_BY_MOVIE_ID = SQL_SELECT +  " WHERE movie_id=@movie_id";
        public static String SQL_SELECT_BY_Storage_ID = SQL_SELECT +  " WHERE storage_id=@storage_id";
        public static String SQL_INSERT = "Insert into " + TableName + " (movie_id, storage_id, ammount) values (@movie_id," +
                                          " @storage_id, @ammount)";
        public static String SQL_REDUCE_AMMOUNT = "ReduceAmount";
        
        public static Collection<Stock> SelectByMovieId(int id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_MOVIE_ID);
            command.Parameters.AddWithValue("@movie_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<Stock> result = ReadMovie(reader);
            reader.Close();
            db.Close();

            foreach (Stock stock in result)
            {
                stock.storage = StorageDAO.SelectById(stock.storage.storage_id);
            }
            
            return result;
        }
        public static Collection<Stock> SelectByStorageId(int id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_Storage_ID);
            command.Parameters.AddWithValue("@storage_id",id);
            SqlDataReader reader = db.Select(command);
            
            Collection<Stock> result = ReadStock(reader);
            reader.Close();
            db.Close();

            foreach (Stock stock in result)
            {
                stock.movie = MovieDAO.SelectById(stock.movie.movie_id);
            }
            
            return result;
        }
        public static Collection<Stock> Read(SqlDataReader reader)
        {
            Collection<Stock> result = new Collection<Stock>();
            while (reader.Read())
            {
                Stock stock = new Stock();
                stock.movie = new Movie();
                stock.movie.movie_id = Convert.ToInt32(reader["movie_id"]);
                stock.storage = new Storage();
                stock.storage.storage_id = Convert.ToInt32(reader["storage_id"]);
                
                stock.ammount = Convert.ToInt32(reader["ammount"]);
                
                result.Add(stock);
            }
            return result;
        }
        
        public static Collection<Stock> ReadStock(SqlDataReader reader)
        {
            Collection<Stock> result = new Collection<Stock>();
            while (reader.Read())
            {
                Stock stock = new Stock();
                stock.movie = new Movie();
                stock.movie.movie_id = Convert.ToInt32(reader["movie_id"]); 
                stock.ammount = Convert.ToInt32(reader["ammount"]);
                result.Add(stock);
            }
            return result;
        }
        
        public static Collection<Stock> ReadMovie(SqlDataReader reader)
        {
            Collection<Stock> result = new Collection<Stock>();
            while (reader.Read())
            {
                Stock stock = new Stock();
                stock.storage = new Storage();
                stock.storage.storage_id = Convert.ToInt32(reader["storage_id"]); 
                stock.ammount = Convert.ToInt32(reader["ammount"]);
                result.Add(stock);
            }
            return result;
        }

        public static Collection<Stock> Select()
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Stock> result = Read(reader);
            
            db.Close();
            reader.Close();
            foreach (var res in result)
            {
                res.movie = MovieDAO.SelectById(res.movie.movie_id);
                res.storage = StorageDAO.SelectById(res.storage.storage_id);
            }
            
            return result;
        }
        
        public static void Insert(StockPost stock)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@storage_id", stock.storage_id);
            command.Parameters.AddWithValue("@movie_id", stock.movie_id);
            command.Parameters.AddWithValue("@ammount", stock.ammount);

            db.ExecuteNonQuery(command);
        }

        public static void Reduce(int movie_id, int storage_id ,int amount)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_REDUCE_AMMOUNT);
            command.CommandType = CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@movie_id", movie_id);
            command.Parameters.AddWithValue("@storage_id",storage_id );
            command.Parameters.AddWithValue("@reducer", amount);
        
            command.ExecuteNonQuery();
            
            db.Close();
        }
    }
}