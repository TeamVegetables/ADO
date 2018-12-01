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

            command.CommandText = "SELECT COUNT(*) AS EmployeesAmount FROM Employees WHERE City = 'London';";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.Append($"{ reader["FirstName"]} {reader["LastName"]} {reader["Age"]}\n");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
