using ADO.Interfaces;
using System.Data;
using System.Text;

namespace ADO.Queries
{

    public class Ninth : IQuery
    {
        private readonly IDbConnection _connection;

        public Ninth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the first and last name(s) of the eldest employee(s)";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            
            command.CommandText = string.Concat("SELECT TOP 1 FirstName, LastName ",
                "FROM Employees ",
                "ORDER BY BirthDate;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.Append($"{reader["FirstName"]}\t{ reader["LastName"]}\n");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
