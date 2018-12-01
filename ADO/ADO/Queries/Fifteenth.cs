using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Fifteenth : IQuery
    {
        private readonly IDbConnection _connection;

        public Fifteenth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show first and last names of the employees "
                    + "as well as the count of orders each of them have received during the year 1997";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = string.Concat("SELECT E.FirstName, E.LastName, COUNT(O.EmployeeID) AS OrdersAmount ",
                "FROM Employees AS E ",
                "JOIN Orders AS O ON O.EmployeeID = E.EmployeeID ",
                "WHERE O.OrderDate BETWEEN '1997-01-01' AND '1997-12-31' ",
                "GROUP BY E.FirstName, E.LastName;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.Append($"{reader["FirstName"]}  {reader["LastName"]}  {reader["OrdersAmount"]}\n");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
