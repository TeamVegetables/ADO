using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Eighteenth: IQuery
    {
        private readonly IDbConnection _connection;

        public Eighteenth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of french customers’ names who have made more than one order (use grouping) ";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT C.ContactName " +
                                  "FROM Customers AS C " +
                                  "JOIN Orders AS O ON O.CustomerID = C.CustomerID " +
                                  "WHERE C.Country = 'France' " +
                                  "GROUP BY C.ContactName " +
                                  "HAVING COUNT (O.CustomerID) > 1;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("{0}\n",reader["ContactName"]);
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
