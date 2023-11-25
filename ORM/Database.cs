using System;
using System.Data.SqlClient;

namespace ORM
{
    public class Database
    {
        public static SqlConnectionStringBuilder BuildSqlConnectionStringBuilderBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "DESKTOP-A3C5HFB\\SQLEXPRESS";
            builder.UserID = "admin";
            builder.Password = "admin";
            builder.InitialCatalog = "vis_project";
            builder.TrustServerCertificate = true;
            return builder;
        }
        public SqlConnection SQLConnection { get; set; }
        public SqlTransaction SQLTransaction { get; set; }
    
        public Database()
        {
            SQLConnection = new SqlConnection();
        }
    
        public bool Connect()
        {
            if (SQLConnection.State != System.Data.ConnectionState.Open)
            {
                SqlConnectionStringBuilder builder = BuildSqlConnectionStringBuilderBuilder();
                SQLConnection.ConnectionString = builder.ConnectionString;
                SQLConnection.Open();
            }   
            return true;
        }

        public void Close()
        {
            SQLConnection.Close();
        }
    
        public int ExecuteNonQuery(SqlCommand command)
        {
            int rowNumber = 0;
            try
            {
                rowNumber = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            return rowNumber;
        }
    
        public SqlCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, SQLConnection);

            if (SQLTransaction != null)
            {
                command.Transaction = SQLTransaction;
            }
            return command;
        }
    
        public SqlDataReader Select(SqlCommand command)
        {
            SqlDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }
    }
}