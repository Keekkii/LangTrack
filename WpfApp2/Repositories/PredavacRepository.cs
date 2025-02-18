using System;
using System.Collections.Generic;
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
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM dbo.Predavac WHERE Id = @id";  // Ensure dbo is there
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    int count = (int)command.ExecuteScalar();
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

        // Add Predavac to the database
        public bool AddPredavacToDatabase(int id, string name, string surname, string subject)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO dbo.Predavac (Id, Name, Surname, Subject) VALUES (@id, @name, @surname, @subject)";
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@surname", System.Data.SqlDbType.NVarChar).Value = surname;
                    command.Parameters.Add("@subject", System.Data.SqlDbType.NVarChar).Value = subject;

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Predavac to database: " + ex.Message);
                }
            }
        }

        // Delete Predavac from the database
        public bool DeletePredavacById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM dbo.Predavac WHERE Id = @id";
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Predavac from database: " + ex.Message);
                }
            }
        }

        // Update Predavac data in the database
        public bool UpdatePredavac(int id, string name, string surname, string subject)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE dbo.Predavac SET Name = @name, Surname = @surname, Subject = @subject WHERE Id = @id";
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@surname", System.Data.SqlDbType.NVarChar).Value = surname;
                    command.Parameters.Add("@subject", System.Data.SqlDbType.NVarChar).Value = subject;

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Predavac in database: " + ex.Message);
                }
            }
        }
    }
}
