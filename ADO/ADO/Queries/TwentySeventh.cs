using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class TwentySeventh : IQuery
    {
        private readonly IDbConnection _connection;

        public TwentySeventh(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of product categories along with total ordering sums calculated " +
                    "for the orders made for the products of each category, during the year 1997.";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT C.CategoryName, P.ProductName, COUNT (O.OrderID) AS OrdersAmount " +
                                  "FROM Categories AS C " +
                                  "LEFT JOIN Products AS P ON P.CategoryID = C.CategoryID " +
                                  "LEFT JOIN [Order Details] AS OD ON OD.ProductID = P.ProductID " +
                                  "LEFT JOIN Orders AS O ON O.OrderID = OD.OrderID " +
                                  "WHERE O.OrderDate BETWEEN '1997-01-01' AND '1997-12-31' " +
                                  "GROUP BY P.ProductName, C.CategoryName;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0}\t{1}\t{2}\n", reader["CategoryName"], reader["ProductName"], reader["OrdersAmount"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}

