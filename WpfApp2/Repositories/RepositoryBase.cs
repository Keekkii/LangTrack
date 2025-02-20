using System;
using System.Data.SQLite;
using System.IO;

namespace WpfApp2.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase()
        {
            string dbFile = "database.db"; // SQLite database file
            _connectionString = $"Data Source={dbFile};Version=3;";

            // Ensure the database file exists
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
            }

            EnsureDatabaseSetup(); // Ensure tables exist
        }

        protected SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        private void EnsureDatabaseSetup()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string createUserTableQuery = @"
                CREATE TABLE IF NOT EXISTS User (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    Name TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    Email TEXT NOT NULL
                );";

                string createPolaznikTableQuery = @"
                CREATE TABLE IF NOT EXISTS Polaznik (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Surname TEXT NOT NULL,
                    Course TEXT NOT NULL
                );";

                string createPredavacTableQuery = @"
                CREATE TABLE IF NOT EXISTS Predavac (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Surname TEXT NOT NULL,
                    Subject TEXT NOT NULL
                );";

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = createUserTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPolaznikTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPredavacTableQuery;
                    command.ExecuteNonQuery();
                }

                EnsureAdminUser(connection); // Ensure an admin user exists
            }
        }

        private void EnsureAdminUser(SQLiteConnection connection)
        {
            string checkUserExistsQuery = "SELECT COUNT(*) FROM User WHERE Username = 'admin'";

            using (var checkCommand = new SQLiteCommand(checkUserExistsQuery, connection))
            {
                int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (userCount == 0)
                {
                    string insertUserQuery = @"
                    INSERT INTO User (Username, Password, Name, LastName, Email) 
                    VALUES ('admin', 'admin123', 'Admin', 'User', 'admin@example.com')";

                    using (var insertCommand = new SQLiteCommand(insertUserQuery, connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }

                    Console.WriteLine("Default admin user created: Username = admin, Password = admin123");
                }
            }
        }
    }
}
