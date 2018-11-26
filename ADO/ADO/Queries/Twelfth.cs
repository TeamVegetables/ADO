using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Twelfth : IQuery
    {
        private readonly IDbConnection _connection;

        public Twelfth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show first, last names and dates of birth of the employees who celebrate their birthdays this month";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT FirstName, LastName, BirthDate FROM Employees WHERE MONTH(BirthDate) = 12;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0,-10}{1,-10}{2}\n", reader["FirstName"], reader["LastName"], reader["BirthDate"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
