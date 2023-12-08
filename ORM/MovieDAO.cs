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
        public static String SQL_INSERT = "Insert into " + TableName + " (movie_name, price_per_day, picture_path) values (@movie_name," +
                                          " @price_per_day, @picture_path)";
        public static String SQL_SELECT_SUB = "select TOP 10 m.* from movie m inner join stock s on m.movie_id = s.movie_id where s.storage_id = @storage and lower(m.movie_name) LIKE @name; ";
        public static String SQL_SELECT_SUB_Only = "select TOP 5 * from movie where lower(movie_name) LIKE @name";

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
        
        public static Collection<Movie> SelectByName(string sub, int storage)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_SUB);
            command.Parameters.AddWithValue("@name", $"%{sub}%");
            command.Parameters.AddWithValue("@storage", storage);
            SqlDataReader reader = db.Select(command);
            Collection<Movie> result = Read(reader);
            reader.Close();
            reader.Dispose();
            command.Dispose();
            db.Close();
            
            foreach (Movie movie in result)
            {
                movie.actors =  MovieActorDAO.SelectActors(movie.movie_id);
            }
            
            return result;
        }
        
        public static Collection<Movie> Search(string sub)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_SUB_Only);
            command.Parameters.AddWithValue("@name", $"%{sub}%");
            SqlDataReader reader = db.Select(command);
            Collection<Movie> result = Read(reader);
            reader.Close();
            reader.Dispose();
            command.Dispose();
            db.Close();
            
            foreach (Movie movie in result)
            {
                movie.actors =  MovieActorDAO.SelectActors(movie.movie_id);
            }
            
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
                movie.picture_path = Convert.ToString(reader["picture_path"]);
                
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
            command.Parameters.AddWithValue("@picture_path", movie.picture_path);
            
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