using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Windows;

namespace Client
{
    public class ResponceRepository
    {
        private const string databasename = "responce.db";
        private SQLiteConnection dbConnect;
        private SQLiteCommand dbCommand;
        private const string sqlSelectLastResponce = "Select responceJson from Responces";
        private const string sqlUpdateLastResponce = "UPDATE Responces SET responceJson =";
        public ResponceRepository()
        {
            dbConnect = new SQLiteConnection();
            dbCommand = new SQLiteCommand();
        }
        public void Connect()
        {
            if (!File.Exists(databasename))
            {
                SQLiteConnection.CreateFile(databasename);
                dbConnect.ConnectionString = "Data Source=" + databasename + ";Version=3;";
                dbConnect.Open();
                dbCommand.Connection = dbConnect;
                dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Responces (id INTEGER PRIMARY KEY AUTOINCREMENT, responceJson TEXT)";
                dbCommand.ExecuteNonQuery();
                dbCommand.CommandText = "INSERT INTO Responces(responceJson) " + $"VALUES ('[]');";
                dbCommand.ExecuteNonQuery();
            }
            else
            {
                dbConnect.ConnectionString = "Data Source=" + databasename + ";Version=3;";
                dbConnect.Open();
            }         
        }
        public string GetLastResponce()
        {
            string result = "";
            if (dbConnect.State != ConnectionState.Open)
            {
                Connect();
            }
            dbCommand.Connection = dbConnect;
            dbCommand.CommandText = sqlSelectLastResponce;
            var reader = dbCommand.ExecuteReader();
            while (reader.Read())
            {
                result = reader["responceJson"].ToString();
            }
            return result;
        }
        public void UpdateLastRespoonce(string responce)
        {
            if (dbConnect.State != ConnectionState.Open)
            {
                Connect();
            }
            string sqlquery = "UPDATE Responces SET responceJson =" + $"'{responce}'" + " where id=1;";
            dbCommand.CommandText = sqlquery;
            dbCommand.ExecuteNonQuery();
        }
    }
}
