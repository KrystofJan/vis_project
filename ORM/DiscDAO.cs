using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class DiscDAO
    {
        public static String TableName = "Discs";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_BY_RENTAL_ID = SQL_SELECT +  " WHERE rental_id=@rental_id";
        public static String SQL_SELECT_BY_MOVIE_ID = SQL_SELECT +  " WHERE movie_id=@movie_id";
        public static String SQL_INSERT = "Insert into " + TableName + " (movie_id, rental_id, ammount) values (@movie_id," +
                                          " @rental_id, @ammount)";
        
        public static Collection<Disc> SelectByMovieId(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_MOVIE_ID);
            command.Parameters.AddWithValue("@movie_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<Disc> result = Read(reader);
            db.Close();
            return result;
        }
        public static Collection<Disc> SelectByRentalId(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_RENTAL_ID);
            command.Parameters.AddWithValue("@rental_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<Disc> result = Read(reader);
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
                disc.movie = MovieDAO.SelectById(movie_id);
                disc.rental = RentalDAO.SelectById(rental_id);
                disc.ammount = Convert.ToInt32(reader["ammount"]);
                
                result.Add(disc);
            }
            return result;
        }

        public static Collection<Disc> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Disc> storage = Read(reader);
            db.Close();
            return storage;
        }
        
        public static void Insert(Disc disc)
        {
            Database db = new Database();
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