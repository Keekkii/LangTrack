using System;
using System.Collections.Generic;
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
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM dbo.Polaznik WHERE Id = @id";  // Ensure dbo is there
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    int count = (int)command.ExecuteScalar();
                    exists = count > 0;
                }
                catch (Exception ex)
                {
                    // Log the full exception to get more info
                    Console.WriteLine($"Error: {ex.Message}");
                    throw new Exception("Error checking if Polaznik exists: " + ex.Message);
                }
            }
            return exists;
        }



        // Add Polaznik to the database
        public bool AddPolaznikToDatabase(int id, string name, string surname, string course)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO dbo.Polaznik (Id, Name, Surname, Course) VALUES (@id, @name, @surname, @course)"; // Use Polaznik here
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@surname", System.Data.SqlDbType.NVarChar).Value = surname;
                    command.Parameters.Add("@course", System.Data.SqlDbType.NVarChar).Value = course;

                    int result = command.ExecuteNonQuery(); // Returns number of rows affected
                    return result > 0; // If rows are affected, insertion was successful
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Polaznik to database: " + ex.Message);
                }
            }
        }

        // Delete Polaznik from the database
        public bool DeletePolaznikById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM dbo.Polaznik WHERE Id = @id";
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    int result = command.ExecuteNonQuery(); // Returns number of rows affected
                    return result > 0; // If rows are affected, deletion was successful
                }
                catch (Exception ex)
                {
                    // Optionally show an error message or log the error
                    throw new Exception("Error deleting Polaznik from database: " + ex.Message);
                }
            }
        }

        // Update Polaznik data in the database
        public bool UpdatePolaznik(int id, string name, string surname, string course)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE dbo.Polaznik SET Name = @name, Surname = @surname, Course = @course WHERE Id = @id";
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@surname", System.Data.SqlDbType.NVarChar).Value = surname;
                    command.Parameters.Add("@course", System.Data.SqlDbType.NVarChar).Value = course;

                    int result = command.ExecuteNonQuery(); // Returns number of rows affected
                    return result > 0; // If rows are affected, update was successful
                }
                catch (Exception ex)
                {
                    // Optionally show an error message or log the error
                    throw new Exception("Error updating Polaznik in database: " + ex.Message);
                }
            }
        }

    }
}

