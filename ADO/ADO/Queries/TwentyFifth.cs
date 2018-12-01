using System;
using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class TwentyFifth : IQuery
    {
        private readonly IDbConnection _connection;

        public TwentyFifth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the total ordering sum calculated for each country of customer";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
        command.CommandText = String.Concat("SELECT C.Country, SUM(OD.UnitPrice * OD.Quantity) AS TotalPrice ",
                "FROM Customers AS C ",
                "LEFT JOIN Orders AS O ON O.CustomerID = C.CustomerID ",
                "LEFT JOIN [Order Details] AS OD ON O.OrderID = OD.OrderID ",
                "GROUP BY C.Country;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.Append($"{reader["Country"]}  {reader["TotalPrice"]}\n");
                }
            }
            return stringBuilder.ToString();
        }
    }
}
