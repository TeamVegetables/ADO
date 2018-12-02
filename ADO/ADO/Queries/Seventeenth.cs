using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Seventeenth : IQuery
    {
        private readonly IDbConnection _connection;

        public Seventeenth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the count of orders made by each customer from France.";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT C.ContactName, COUNT(O.CustomerID) AS OrdersAmount " +
                                  "FROM Customers AS C " +
                                  "JOIN Orders AS O ON O.CustomerID = C.CustomerID " +
                                  "WHERE C.Country = 'France' " +
                                  "GROUP BY C.ContactName;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0}\t{1}\n", reader["ContactName"], reader["OrdersAmount"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}

