using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Eigth : IQuery
    {
        private readonly IDbConnection _connection;

        public Eigth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of cities in which the average age of employees is greater than 60 ";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT City, AVG(DATEDIFF(year, BirthDate, GETDATE())) AS AvgBirth" +
                                  " FROM Employees " +
                                  "GROUP BY City " +
                                  "HAVING AVG(DATEDIFF(year, BirthDate, GETDATE())) > 60;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("{0}\t{1}\n", reader["City"], reader["AvgBirth"]);
                    }
                }
            }

            return stringBuilder.ToString();
        }

    }
}