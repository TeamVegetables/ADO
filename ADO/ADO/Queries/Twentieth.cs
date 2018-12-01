using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Twentieth : IQuery
    {
        private readonly IDbConnection _connection;

        public Twentieth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of customers’ names who used to order the ‘Tofu’ product.";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = string.Concat("SELECT C.ContactName ",
                "FROM Customers AS C ",
                "JOIN Orders AS O ON O.CustomerID = C.CustomerID ",
                "JOIN [Order Details] AS OD ON O.OrderID = OD.OrderID ",
                "JOIN Products AS P ON P.ProductID = OD.ProductID ",
                "WHERE P.ProductName = 'Tofu';");
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
