using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using WpfApp2.Repositories;

namespace WpfApp2.Repositories
{
    internal class PredavacRepository : RepositoryBase
    {
        public bool IsPredavacExists(int id)
        {
            bool exists = false;
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM Predavac WHERE Id = @id"; 
                    command.Parameters.Add("@id", DbType.Int32).Value = id;

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    exists = count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw new Exception("Error checking if Predavac exists: " + ex.Message);
                }
            }
            return exists;
        }

        // Dodavanje predavaca u bazu
        public bool AddPredavacToDatabase(int id, string name, string surname, string subject)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Predavac (Id, Name, Surname, Subject) VALUES (@id, @name, @surname, @subject)";
                    command.Parameters.Add("@id", DbType.Int32).Value = id;
                    command.Parameters.Add("@name", DbType.String).Value = name;
                    command.Parameters.Add("@surname", DbType.String).Value = surname;
                    command.Parameters.Add("@subject", DbType.String).Value = subject;

                    int result = command.ExecuteNonQuery();
                    return result > 0; //Uvjet uspjesnosti
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Predavac to database: " + ex.Message);
                }
            }
        }

        // Brisanje predavaca 
        public bool DeletePredavacById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM Predavac WHERE Id = @id";
                    command.Parameters.Add("@id", DbType.Int32).Value = id;

                    int result = command.ExecuteNonQuery();
                    return result > 0; //Uvjet uspjesnosti
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Predavac from database: " + ex.Message);
                }
            }
        }

        // Promjena predavaca
        public bool UpdatePredavac(int id, string name, string surname, string subject)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE Predavac SET Name = @name, Surname = @surname, Subject = @subject WHERE Id = @id";
                    command.Parameters.Add("@id", DbType.Int32).Value = id;
                    command.Parameters.Add("@name", DbType.String).Value = name;
                    command.Parameters.Add("@surname", DbType.String).Value = surname;
                    command.Parameters.Add("@subject", DbType.String).Value = subject;

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Predavac in database: " + ex.Message);
                }
            }
        }

        public int GetPredavaciCount()
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM Predavac"; //Broj predavaca u bazi, bitno za statistiku
                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting Predavaci count: " + ex.Message);
                }
            }
        }

    }
}
