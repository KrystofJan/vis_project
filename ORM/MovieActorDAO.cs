using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class MovieActorDAO
    {
        private static Database db = Database.Instance;
        public static String TableName = "movie_actor";
        public static String SQL_SELECT_ACTOR_BY_MOVIE_ID = "SELECT actor_id FROM movie_actor WHERE movie_id=@movie_id";
        public static String SQL_SELECT_MOVIE_BY_ACTOR_ID = "SELECT movie_id FROM movie_actor WHERE actor_id=@actor_id";
        public static String SQL_INSERT = "Insert into " + TableName + " (movie_id, actor_id) values (@movie_id," +
                                          " @actor_id)";
        public static Collection<Actor> SelectActors(int id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ACTOR_BY_MOVIE_ID);
            command.Parameters.AddWithValue("@movie_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<int> result = ReadActor(reader);
            reader.Close();
            db.Close();

            Collection<Actor> actors = new Collection<Actor>();
            foreach (int actorId in result)
            {
                actors.Add(ActorDAO.SelectById(actorId));
            }
            return actors;
        }
        public static Collection<Movie> SelectMovies(int id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_MOVIE_BY_ACTOR_ID);
            command.Parameters.AddWithValue("@actor_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<int> result = ReadMovie(reader);
            db.Close();
            reader.Close();
            
            Collection<Movie> movies = new Collection<Movie>();
            foreach (int movieId in result)
            {
                movies.Add(MovieDAO.SelectById(movieId));
            }
            return movies;
        }
        
        public static Collection<int> ReadActor(SqlDataReader reader)
        {
            Collection<int> result = new Collection<int>();
            while (reader.Read())
            {
                result.Add(Convert.ToInt32(reader["actor_id"])); 
            }
            return result;
        }
        public static Collection<int> ReadMovie(SqlDataReader reader)
        {
            Collection<int> result = new Collection<int>();
            while (reader.Read())
            {
                result.Add(Convert.ToInt32(reader["movie_id"]));
            }
            return result;
        }
        
        public static void Insert(int movie_id, int actor_id)
        {
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@movie_id", movie_id);
            command.Parameters.AddWithValue("@actor_id", actor_id);
            
            int ret = db.ExecuteNonQuery(command);
            
            db.Close();
        }
        
    }
}