using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Thirteenth : IQuery
    {
        private readonly IDbConnection _connection;

        public Thirteenth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show first and last names of the employees who used to serve orders shipped to Madrid";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT FirstName, LastName FROM Employees " +
                                  "JOIN Orders ON Employees.EmployeeID = Orders.EmployeeID " +
                                  "WHERE ShipCity = 'Madrid'; ";
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

