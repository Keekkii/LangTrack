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
                    SELECT Id, Name, Surname, 'Polaznik' AS Role, Course AS Course FROM Polaznik
                    UNION ALL
                    SELECT Id, Name, Surname, 'Predavac' AS Role, Subject AS Course FROM Predavac;
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
                            Role = reader["Role"].ToString(),
                            Course = reader["Course"].ToString()
                        });
                    }
                }
            }

            return allData;
        }

        // Method to delete data from the database
        public void DeleteData(TablicaModel item)
        {
            if (item == null)
            {
                Console.WriteLine("Item to delete is null.");
                return;
            }

            Console.WriteLine($"Attempting to delete: Id = {item.Id}, Role = {item.Role}");

            using (var connection = GetConnection())
            {
                connection.Open();

                string query = string.Empty;

                if (item.Role == "Polaznik")
                {
                    query = "DELETE FROM Polaznik WHERE Id = @Id";
                }
                else if (item.Role == "Predavac")
                {
                    query = "DELETE FROM Predavac WHERE Id = @Id";
                }

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
