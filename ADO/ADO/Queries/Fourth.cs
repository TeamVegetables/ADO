using System.Data;
using System.Text;
using ADO.Interfaces;


namespace ADO.Queries
{
    public class Fourth : IQuery
    {
        private readonly IDbConnection _connection;

        public Fourth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of first, last names and ages of the employees whose age is greater than 55";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT FirstName, LastName, DATEDIFF(year, BirthDate, GETDATE()) AS Age " +
                                  "FROM " +
                                  "Employees" +
                                  " WHERE DATEDIFF(year, BirthDate, GETDATE()) > 55 " +
                                  "ORDER BY LastName;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("{0, -20} {1}\t{2}\n", reader["FirstName"], reader["LastName"], reader["Age"]);
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
