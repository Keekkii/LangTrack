using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Net;
using WpfApp2.Model;

namespace WpfApp2.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser = false;
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM User WHERE username = @username AND password = @password";
                command.Parameters.AddWithValue("@username", credential.UserName);
                command.Parameters.AddWithValue("@password", credential.Password);

                int count = Convert.ToInt32(command.ExecuteScalar());
                validUser = count > 0;
            }
            return validUser;
        }

        public void Edit(UserModel user) { throw new NotImplementedException(); }

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM User WHERE username = @username";
                command.Parameters.AddWithValue("@username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader["Id"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = string.Empty, // Don't return password for security reasons
                            Name = reader["Name"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
