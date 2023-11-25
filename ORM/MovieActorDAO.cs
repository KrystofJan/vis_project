using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class MovieActorDAO
    {
        public static String TableName = "movie_actor";
        public static String SQL_SELECT_ACTOR_BY_MOVIE_ID = "SELECT actor_id FROM movie_actor WHERE movie_id=@movie_id";
        public static String SQL_SELECT_MOVIE_BY_ACTOR_ID = "SELECT movie_id FROM movie_actor WHERE actor_id=@actor_id";
        public static String SQL_INSERT = "Insert into " + TableName + " (movie_id, actor_id) values (@movie_id," +
                                          " @actor_id)";
        public static Collection<Actor> SelectActors(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ACTOR_BY_MOVIE_ID);
            command.Parameters.AddWithValue("@movie_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<Actor> result = ReadActor(reader);
            db.Close();
            return result;
        }
        public static Collection<Movie> SelectMovies(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_MOVIE_BY_ACTOR_ID);
            command.Parameters.AddWithValue("@actor_id",id);
            SqlDataReader reader = db.Select(command);
            Collection<Movie> result = ReadMovie(reader);
            db.Close();
            return result;
        }
        
        public static Collection<Actor> ReadActor(SqlDataReader reader)
        {
            Collection<Actor> result = new Collection<Actor>();
            while (reader.Read())
            {
                int actor;

                actor = Convert.ToInt32(reader["actor_id"]);

                Actor actr = ActorDAO.SelectById(actor);
                result.Add(actr);
            }
            return result;
        }
        public static Collection<Movie> ReadMovie(SqlDataReader reader)
        {
            Collection<Movie> result = new Collection<Movie>();
            while (reader.Read())
            {
                int movie;

                movie = Convert.ToInt32(reader["movie_id"]);

                Movie mvi = MovieDAO.SelectById(movie);
                result.Add(mvi);
            }
            return result;
        }
        
        public static void Insert(int movie_id, int actor_id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@movie_id", movie_id);
            command.Parameters.AddWithValue("@actor_id", actor_id);
            
            int ret = db.ExecuteNonQuery(command);
            
            db.Close();
        }
        
    }
}