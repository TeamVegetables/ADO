using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Eleventh : IQuery
    {
        private readonly IDbConnection _connection;

        public Eleventh(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of all cities where the employees are from.";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT DISTINCT City FROM Employees;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0}\n", reader["City"].ToString());
                }
            }
            return stringBuilder.ToString();
        }
    }
}
