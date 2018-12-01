using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Sixteenth : IQuery
    {
        private readonly IDbConnection _connection;

        public Sixteenth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show first and last names of the employees as well as the count of their orders shipped after required date during the year 1997 (use left join).";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = string.Concat("SELECT E.FirstName, E.LastName, COUNT(O.EmployeeID) AS OrdersAmount ",
                "FROM Employees AS E ",
                "LEFT JOIN Orders AS O ON O.EmployeeID = E.EmployeeID ",
                "WHERE O.ShippedDate > O.RequiredDate ",
                "AND O.OrderDate BETWEEN '1997-01-01' AND '1997-12-31' ",
                "GROUP BY E.FirstName, E.LastName;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("{0,-10}{1,-10}{2}\n", reader["FirstName"], reader["LastName"], reader["OrdersAmount"]);
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
