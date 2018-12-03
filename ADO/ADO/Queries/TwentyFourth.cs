using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class TwentyFourth:IQuery
    {
        private readonly IDbConnection _connection;

        public TwentyFourth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of french customers’ names who used to order french products";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT DISTINCT C.ContactName " +
                                  "FROM Customers AS C " +
                                  "LEFT JOIN Orders AS O ON C.CustomerID = O.CustomerID " +
                                  "LEFT JOIN [Order Details] AS OD ON O.OrderID = OD.OrderID " +
                                  "LEFT JOIN [Products] AS P ON OD.ProductID = P.ProductID " +
                                  "LEFT JOIN [Suppliers] AS S ON P.SupplierID = S.SupplierID " +
                                  "WHERE S.Country = 'France' AND C.Country ='France';";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("{0}\n", reader["ContactName"]);
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
