using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Nineteenth : IQuery
    {
        private readonly IDbConnection _connection;

        public Nineteenth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of french customers’ names who have made more than one order.";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = string.Concat("SELECT C.ContactName ",
                "FROM Customers AS C ",
                "WHERE C.Country = 'France' ",
                "AND C.CustomerID IN (SELECT DISTINCT CustomerID FROM Orders);");
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
