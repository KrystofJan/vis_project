using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Models;

namespace ORM
{
    public class ActorDAO
    {
        public static String TableName = "Actor";
        public static String SQL_SELECT = "SELECT * FROM "+ TableName;
        public static String SQL_SELECT_ID = SQL_SELECT + " WHERE actor_id=@actor_id";

        public static String SQL_INSERT = "Insert into " + TableName + " (first_name, last_name) values (@first_name," +
                                          " @last_name)";
        
        public static Actor SelectById(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
           
            command.Parameters.AddWithValue("@actor_id",id);
            SqlDataReader reader = db.Select(command);
            Actor result = Read(reader)[0];
            db.Close();
            return result;

        }
        
        public static Collection<Actor> Read(SqlDataReader reader)
        {
            Collection<Actor> result = new Collection<Actor>();
            while (reader.Read())
            {
                Actor actor = new Actor();
                
                actor.actor_id = Convert.ToInt32(reader["actor_id"]);
                actor.first_name = Convert.ToString(reader["first_name"]);
                actor.last_name = Convert.ToString(reader["last_name"]);

                result.Add(actor);
            }
            return result;
        }

        public static Collection<Actor> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Actor> result = Read(reader);
            db.Close();
            return result;

        }
        
        public static int Insert(Actor actor)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);

            command.Parameters.AddWithValue("@first_name", actor.first_name);
            command.Parameters.AddWithValue("@last_name", actor.last_name);
            
            int ret = db.ExecuteNonQuery(command);
            
            SqlCommand command2 = db.CreateCommand("SELECT IDENT_CURRENT('Actor')");
            int id = Convert.ToInt32(command2.ExecuteScalar());
            actor.actor_id = id;
            db.Close();
            return id;
        }
    }
}