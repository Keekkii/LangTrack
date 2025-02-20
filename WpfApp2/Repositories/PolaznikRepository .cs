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
    internal class PolaznikRepository : RepositoryBase
    {
        public bool IsPolaznikExists(int id)
        {
            bool exists = false;
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM Polaznik WHERE Id = @id";  
                    command.Parameters.Add("@id", DbType.Int32).Value = id;

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    exists = count > 0;
                }
                catch (Exception ex)
                {
                    // Ispis exceptiona 
                    Console.WriteLine($"Error: {ex.Message}");
                    throw new Exception("Error checking if Polaznik exists: " + ex.Message);
                }
            }
            return exists;
        }



        // Dodavanje polaznika u bazu
        public bool AddPolaznikToDatabase(int id, string name, string surname, string course)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Polaznik (Id, Name, Surname, Course) VALUES (@id, @name, @surname, @course)"; 
                    command.Parameters.Add("@id", DbType.Int32).Value = id;
                    command.Parameters.Add("@name", DbType.String).Value = name;
                    command.Parameters.Add("@surname", DbType.String).Value = surname;
                    command.Parameters.Add("@course", DbType.String).Value = course;

                    int result = command.ExecuteNonQuery(); 
                    return result > 0; // Uvjet uspjesnog upisa
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Polaznik to database: " + ex.Message);
                }
            }
        }

        // Brisanje polaznika iz baze
        public bool DeletePolaznikById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM Polaznik WHERE Id = @id";
                    command.Parameters.Add("@id", DbType.Int32).Value = id;

                    int result = command.ExecuteNonQuery(); 
                    return result > 0; // Uvjet uspjesnog brisanja
                }
                catch (Exception ex)
                {
                    // Greska pri brisanju
                    throw new Exception("Error deleting Polaznik from database: " + ex.Message);
                }
            }
        }

        // Promjena polaznika
        public bool UpdatePolaznik(int id, string name, string surname, string course)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE Polaznik SET Name = @name, Surname = @surname, Course = @course WHERE Id = @id";
                    command.Parameters.Add("@id", DbType.Int32).Value = id;
                    command.Parameters.Add("@name", DbType.String).Value = name;
                    command.Parameters.Add("@surname", DbType.String).Value = surname;
                    command.Parameters.Add("@course", DbType.String).Value = course;

                    int result = command.ExecuteNonQuery(); 
                    return result > 0; // Uvjet uspesnosti
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Polaznik in database: " + ex.Message);
                }
            }
        }

        public int GetPolazniciCount()
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM Polaznik"; // Upit za brojanje polaznika, koristi se za statistiku
                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting Polaznici count: " + ex.Message);
                }
            }
        }


    }
}

