using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class DiscDAO
    {
        private static Database db = Database.Instance;
        public static String TableName = "Discs";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_ALL = "SELECT * FROM "+ TableName + " Where movie_id=@movie_id and rental_id=@rental_id";
        public static String SQL_SELECT_BY_RENTAL_ID = SQL_SELECT +  " WHERE rental_id=@rental_id";
        public static String SQL_SELECT_BY_MOVIE_ID = SQL_SELECT +  " WHERE movie_id=@movie_id";
        public static String SQL_INSERT = "Insert into " + TableName + " (movie_id, rental_id, ammount) values (@movie_id," +
                                          " @rental_id, @ammount)";

        public static String SQL_Update =
            "update discs set ammount = ammount-1 where rental_id = @rental_id and movie_id = @movie_id";
        
        public static Collection<Disc> SelectByMovieId(int id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_MOVIE_ID);
            command.Parameters.AddWithValue("@movie_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<Disc> result = Read(reader);
            db.Close();
            reader.Close();
            return result;
        }

        public static int Update(int rental_id, int movie_id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_Update);
            command.Parameters.AddWithValue("@rental_id",rental_id);            
            command.Parameters.AddWithValue("@movie_id",movie_id);

            int reader = db.ExecuteNonQuery(command);
            db.Close();

            return reader;
        }
        public static Collection<Disc> SelectByRentalId(int id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_RENTAL_ID);
            command.Parameters.AddWithValue("@rental_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<Disc> result = Read(reader);
            reader.Close();
            db.Close();
            return result;
        }
        
        public static Collection<Disc> SelectAll(int idm, int idr)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            command.Parameters.AddWithValue("@movie_id",idm);
            command.Parameters.AddWithValue("@rental_id",idr);
            SqlDataReader reader = db.Select(command);
            Collection<Disc> result = Read(reader);
            reader.Close();
            db.Close();
            return result;
        }
        
        public static Collection<Disc> Read(SqlDataReader reader)
        {
            Collection<Disc> result = new Collection<Disc>();
            while (reader.Read())
            {
                Disc disc = new Disc();

                int movie_id = Convert.ToInt32(reader["movie_id"]);
                int rental_id = Convert.ToInt32(reader["rental_id"]);
                disc.movie = new Movie();
                disc.movie.movie_id = movie_id;
                disc.rental = new Rental();
                disc.rental.rental_id = rental_id;
                disc.ammount = Convert.ToInt32(reader["ammount"]);
                
                result.Add(disc);
            }
            return result;
        }

        public static Collection<Disc> Select()
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Disc> storage = Read(reader);
            db.Close();
            reader.Close();
            return storage;
        }
        
        public static void Insert(Disc disc)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@rental_id", disc.rental.rental_id);
            command.Parameters.AddWithValue("@movie_id", disc.movie.movie_id);
            command.Parameters.AddWithValue("@ammount", disc.ammount);
            
            db.ExecuteNonQuery(command);
            db.Close();
        }
    }
}