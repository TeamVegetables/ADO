using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Thirtieth : IQuery
    {
        private readonly IDbConnection _connection;

        public Thirtieth(IDbConnection connection)
        {
            _connection = connection;
            Title =
                "Show the list of cities where employees and customers are" +
                " from and where orders have been made to. Duplicates should be eliminated.";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "(SELECT E.City AS OrderFromCity, O.ShipCity AS OrderToCity " +
                                  "FROM Orders AS O " +
                                  "LEFT JOIN Employees AS E ON E.EmployeeID = O.EmployeeID " +
                                  "GROUP BY E.City, O.ShipCity) " +
                                  "UNION " +
                                  "(SELECT C.City AS OrderFromCity, O.ShipCity AS OrderToCity " +
                                  "FROM Orders AS O " +
                                  "LEFT JOIN Customers AS C ON C.CustomerID = O.CustomerID " +
                                  "GROUP BY C.City, O.ShipCity);";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0}\t{1}\n", reader["OrderFromCity"], reader["OrderToCity"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}

