using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Seventh : IQuery
    {
        private readonly IDbConnection _connection;

        public Seventh(IDbConnection connection)
        {
            _connection = connection;
            Title = "Calculate the greatest, the smallest and the average age of the employees for each city.";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT MAX(DATEDIFF(year, BirthDate, GETDATE())) AS MaxBirth, " +
                                  "MIN(DATEDIFF(year, BirthDate, GETDATE())) AS MinBirth, " +
                                  "AVG(DATEDIFF(year, BirthDate, GETDATE())) AS AvgBirth " +
                                  "FROM Employees " +
                                  "WHERE City='London';";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0}\t{1}\t{2}\n", reader["MaxBirth"], reader["MinBirth"], reader["AvgBirth"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
