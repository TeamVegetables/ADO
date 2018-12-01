using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class TwentySixth : IQuery
    {
        private readonly IDbConnection _connection;

        public TwentySixth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the total ordering sums calculated for each customer’s country for domestic and non-domestic products separately (e.g.: “France – French products ordered – Non-french products ordered” and so on for each country).";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = string.Concat(
                "SELECT D1.Country, D1.Domestic, D2.NonDomestic ",
                "FROM ",
                "(SELECT C.Country, COUNT (P.ProductID) AS Domestic ",
                "FROM Customers AS C ",
                "LEFT JOIN Orders AS O ON C.CustomerID = O.CustomerID ",
                "LEFT JOIN [Order Details] AS OD ON O.OrderID = OD.OrderID ",
                "LEFT JOIN [Products] AS P ON OD.ProductID = P.ProductID ",
                "LEFT JOIN [Suppliers] AS S ON P.SupplierID = S.SupplierID ",
                "WHERE S.country = C.Country ",
                "GROUP BY C.Country) AS D1 ",
                "LEFT JOIN ",
                "(SELECT C.Country, COUNT (P.ProductID) AS NonDomestic ",
                "FROM Customers AS C ",
                "LEFT JOIN Orders AS O ON C.CustomerID = O.CustomerID ",
                "LEFT JOIN [Order Details] AS OD ON O.OrderID = OD.OrderID ",
                "LEFT JOIN [Products] AS P ON OD.ProductID = P.ProductID ",
                "LEFT JOIN [Suppliers] AS S ON P.SupplierID = S.SupplierID ",
                "WHERE S.country <> C.Country ",
                "GROUP BY C.Country) AS D2 ",
                "ON D1.Country = D2.Country;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("{0, -20}\n\t- {0} products ordered {1} \n\t- Non {0} products ordered {2, -20}\n", reader["Country"], reader["Domestic"], reader["NonDomestic"]);
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
