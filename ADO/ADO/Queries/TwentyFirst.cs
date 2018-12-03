using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class TwentyFirst : IQuery
    {
        private readonly IDbConnection _connection;

        public TwentyFirst(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of customers’ names who used to order the ‘Tofu’ product,\n"
                    + "along with the total amount of the product they have ordered and with the total sum for ordered product calculated";
            ;
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = string.Concat("SELECT C.ContactName, SUM(OD.Quantity) AS Count, SUM(OD.UnitPrice * OD.Quantity) AS PriceSum ",
                "FROM Customers AS C ",
                "LEFT JOIN Orders AS O ON C.CustomerID = O.CustomerID ",
                "LEFT JOIN [Order Details] AS OD ON OD.OrderId = O.OrderId ",
                "LEFT JOIN [Products] AS P ON P.ProductID = OD.ProductID ",
                "WHERE P.ProductName = 'Tofu' ",
                "GROUP BY C.ContactName;");

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0, -20} {1,-3} {2}\n", reader["ContactName"], reader["Count"], reader["PriceSum"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
