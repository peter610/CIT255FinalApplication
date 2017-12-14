using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CIT255FinalApplication
{
    class FirearmRepositorySQL : IFirearmRepository
    {
        private IEnumerable<Firearm> _firearms = new List<Firearm>();

        private IEnumerable<Firearm> ReadAllFirearms()
        {
            IList<Firearm> firearms = new List<Firearm>();

            string connString = GetConnectionString();
            string sqlCommandString = "SELECT * from Firearms";

            using (SqlConnection sqlConn = new SqlConnection(connString))
            using (SqlCommand sqlCommand = new SqlCommand(sqlCommandString, sqlConn))
            {
                try
                {
                    sqlConn.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Firearm firearm = new Firearm();
                                firearm.ID = Convert.ToInt32(reader["ID"]);
                                firearm.Name = reader["Name"].ToString();
                                firearm.Manufacturer = reader["Manufacturer"].ToString();
                                firearm.FirearmType = reader["FirearmType"].ToString();
                                firearm.AmmoType = reader["AmmoType"].ToString();
                                firearm.BarrelLength = Convert.ToInt32(reader["BarrelLength"]);

                                firearms.Add(firearm);
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    Console.WriteLine(sqlCommandString);
                }
            }

            return firearms;
        }

        public FirearmRepositorySQL()
        {
            _firearms = ReadAllFirearms();
        }

        /// <summary>
        /// method to return a list of firearms
        /// uses a DataSet to hold firearm info
        /// </summary>
        /// <returns>list of firearm objects</returns>
        public List<Firearm> SelectAll()
        {
            return _firearms as List<Firearm>;
        }

        /// <summary>
        /// method to return a firearm given the ID
        /// uses a DataSet to hold firearm info
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>firearm object</returns>
        public Firearm SelectById(int Id)
        {
            return _firearms.Where(sr => sr.ID == Id).FirstOrDefault();
        }

        /// <summary>
        /// method to add a new firearm
        /// </summary>
        /// <param name="firearm"></param>
        public void Insert(Firearm firearm)
        {
            string connString = GetConnectionString();

            // build out SQL command
            var sb = new StringBuilder("INSERT INTO Firearms");
            sb.Append(" ([ID],[Name],[Manufacturer], [FirearmType], [AmmoType], [BarrelLength])");
            sb.Append(" Values (");
            sb.Append("'").Append(firearm.ID).Append("',");
            sb.Append("'").Append(firearm.Name).Append("',");
            sb.Append("'").Append(firearm.Manufacturer).Append("',");
            sb.Append("'").Append(firearm.FirearmType).Append("',");
            sb.Append("'").Append(firearm.AmmoType).Append("',");
            sb.Append("'").Append(firearm.BarrelLength).Append("')");
            string sqlCommandString = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(connString))
            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())
            {
                try
                {
                    sqlConn.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    Console.WriteLine(sqlCommandString);
                }
            }
        }

        /// <summary>
        /// method to delete a firearm by ID
        /// </summary>
        /// <param name="ID"></param>
        public void Delete(int ID)
        {
            string connString = GetConnectionString();

            // build out SQL command
            var sb = new StringBuilder("DELETE FROM Firearms");
            sb.Append(" WHERE ID = ").Append(ID);
            string sqlCommandString = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(connString))
            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())
            {
                try
                {
                    sqlConn.Open();
                    sqlAdapter.DeleteCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    Console.WriteLine(sqlCommandString);
                }
            }
        }

        /// <summary>
        /// method to update an existing ski run
        /// </summary>
        /// <param name="firearm">firearm object</param>
        public void Update(Firearm firearm)
        {
            string connString = GetConnectionString();

            // build out SQL command
            var sb = new StringBuilder("UPDATE Firearms SET ");
            sb.Append("Name = '").Append(firearm.Name).Append("', ");
            sb.Append("Manufacturer = '").Append(firearm.Manufacturer).Append("', ");
            sb.Append("FirearmType = '").Append(firearm.FirearmType).Append("', ");
            sb.Append("AmmoType = '").Append(firearm.AmmoType).Append("', ");
            sb.Append("BarrelLength = ").Append(firearm.BarrelLength).Append(" ");
            sb.Append("WHERE ");
            sb.Append("ID = ").Append(firearm.ID);
            string sqlCommandString = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(connString))
            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())
            {
                try
                {
                    sqlConn.Open();
                    sqlAdapter.UpdateCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    Console.WriteLine(sqlCommandString);
                }
            }
        }

        /// <summary>
        /// method to query the data by the firearm ID
        /// </summary>
        /// <param name="lowerId">int minimum vertical</param>
        /// <param name="higherId">int maximum vertical</param>
        /// <returns></returns>
        public IEnumerable<Firearm> QueryById(int lowerId, int higherId)
        {
            return _firearms.Where(sr => sr.ID >= lowerId && sr.ID <= higherId);
        }

        /// <summary>
        /// get the connection string by name
        /// </summary>
        /// <returns>string connection string</returns>
        private static string GetConnectionString()
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            var settings = ConfigurationManager.ConnectionStrings["Firearm_Local"];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

        public void Dispose()
        {
            _firearms = null;
        }
    }
}
