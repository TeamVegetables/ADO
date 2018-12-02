using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class TwentyNinth : IQuery
    {
        private readonly IDbConnection _connection;

        public TwentyNinth(IDbConnection connection)
        {
            _connection = connection;
            Title =
                "Show the list of employees’ names along with " +
                "names of their chiefs (use left join with the same table).";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT E1.LastName AS Name, E2.LastName AS Chief " +
                                  "FROM Employees AS E1 " +
                                  "LEFT JOIN Employees AS E2 ON E1.ReportsTo = E2.EmployeeID;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0}\t{1}\n", reader["Name"], reader["Chief"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}

