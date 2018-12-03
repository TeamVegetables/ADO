using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Sixth : IQuery
    {
        private readonly IDbConnection _connection;

        public Sixth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Calculate the greatest, the smallest and the average age among the employees from London.";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = string.Concat("SELECT MAX(DATEDIFF(year, BirthDate, GETDATE())) AS MaxBirth, ",
                "MIN(DATEDIFF(year, BirthDate, GETDATE())) AS MinBirth, ",
                "AVG(DATEDIFF(year, BirthDate, GETDATE())) AS AvgBirth ",
                "FROM Employees ",
                "WHERE City='London';");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("{0}\t{1}\t{2}\n", reader["MaxBirth"], reader["MinBirth"], reader["AvgBirth"]);
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
