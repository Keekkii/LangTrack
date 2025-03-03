using System;
using System.Collections.Generic;
using System.Data.SQLite;
using WpfApp2.Model;

namespace WpfApp2.Repositories
{
    public class TablicaRepository : RepositoryBase
    {
        // Method to get all users (both Polaznik and Predavac)
        public List<TablicaModel> GetAllData()
        {
            List<TablicaModel> allData = new List<TablicaModel>();

            using (var connection = GetConnection())
            {
                connection.Open();

                // Query both Polaznik and Predavac tables
                string query = @"
                    SELECT Id, Name, Surname, 'Polaznik' AS Uloga, Course AS Tečaj FROM Polaznik
                    UNION ALL
                    SELECT Id, Name, Surname, 'Predavac' AS Uloga, Subject AS Tečaj FROM Predavac;
                ";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allData.Add(new TablicaModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            Role = reader["Uloga"].ToString(),
                            Course = reader["Tečaj"].ToString()
                        });
                    }
                }
            }

            return allData;
        }
    }
}
