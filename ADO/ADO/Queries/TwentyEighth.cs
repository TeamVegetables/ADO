using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class TwentyEighth: IQuery
    {
        private readonly IDbConnection _connection;

        public TwentyEighth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of product names along with unit prices and the history of unit prices taken from the orders " +
                    "(show ‘Product name – Unit price – Historical price’). The duplicate records should be eliminated. " +
                    "If no orders were made for a certain product, then the result for this " +
                    "product should look like ‘Product name – Unit price – NULL’. Sort the list by the product name";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT P.ProductName, P.UnitPrice, OD.UnitPrice AS HistoricalPrice " +
                                  "FROM Products AS P " +
                                  "RIGHT JOIN [Order Details] AS OD ON P.ProductID = OD.ProductID " +
                                  "WHERE P.UnitPrice <> OD.UnitPrice " +
                                  "GROUP BY P.ProductName, P.UnitPrice, OD.UnitPrice " +
                                  "ORDER BY P.ProductName;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("{0, -40} {1, -20} {2}\n", reader["ProductName"], reader["UnitPrice"], reader["HistoricalPrice"]);
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
