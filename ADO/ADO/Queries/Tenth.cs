using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Tenth : IQuery
    {
        private readonly IDbConnection _connection;

        public Tenth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show first, last names and ages of 3 eldest employees";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();

            command.CommandText = string.Concat("SELECT TOP 3 FirstName, LastName, DATEDIFF(year, BirthDate, GETDATE()) AS Age ",
                "FROM Employees ",
                "ORDER BY BirthDate;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0, -10} {1, -10} {2}\n", reader["FirstName"], reader["LastName"], reader["Age"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
