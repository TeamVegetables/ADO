using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Second : IQuery
    {
        private readonly IDbConnection _connection;

        public Second(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of first and last names of the employees from London";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT FirstName, LastName FROM Employees WHERE City = 'London';";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0}\t{1}\n", reader["FirstName"], reader["LastName"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
