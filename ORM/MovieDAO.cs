using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class MovieDAO
    {
        private static Database db = Database.Instance;
        public static String TableName = "Movie";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_ID = SQL_SELECT +  " WHERE movie_id=@movie_id";
        public static String SQL_INSERT = "Insert into " + TableName + " (movie_name, price_per_day) values (@movie_name," +
                                          " @price_per_day)";
        
        public static Movie SelectById(int id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@movie_id",id);
            SqlDataReader reader = db.Select(command);
            Movie result = Read(reader)[0];
            reader.Close();
            reader.Dispose();
            command.Dispose();
            db.Close();
            result.actors = MovieActorDAO.SelectActors(result.movie_id);
            return result;
        }
        
        public static Collection<Movie> Read(SqlDataReader reader)
        {
            Collection<Movie> result = new Collection<Movie>();
            while (reader.Read())
            {
                Movie movie = new Movie();
                
                movie.movie_id = Convert.ToInt32(reader["movie_id"]);
                movie.movie_name = Convert.ToString(reader["movie_name"]);
                movie.price_per_day = Convert.ToDecimal(reader["price_per_day"]);
                
                result.Add(movie);
            }
            return result;
        }

        public static Collection<Movie> Select()
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Movie> m = Read(reader);
            reader.Close();
            db.Close();
            
            foreach (Movie movie in m)
            {
                movie.actors =  MovieActorDAO.SelectActors(movie.movie_id);
            }
            return m;
        }
        
        public static int Insert(Movie movie)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@movie_name", movie.movie_name);
            command.Parameters.AddWithValue("@price_per_day", movie.price_per_day);
            
            int ret = db.ExecuteNonQuery(command);
            
            SqlCommand command2 = db.CreateCommand("SELECT IDENT_CURRENT('Movie')");
            int id = Convert.ToInt32(command2.ExecuteScalar());
            movie.movie_id = id;
            db.Close();
            foreach (Actor act in movie.actors)
            {
                MovieActorDAO.Insert(id,act.actor_id);
            }
            return id;
        }
    }
}