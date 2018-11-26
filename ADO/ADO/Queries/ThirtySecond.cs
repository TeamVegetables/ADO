using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class ThirtySecond : IQuery
    {
        private readonly IDbConnection _connection;

        public ThirtySecond(IDbConnection connection)
        {
            _connection = connection;
            Title = "Fetch the records you have inserted by the SELECT statement";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT * FROM Employees WHERE Notes LIKE 'Adam';";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        stringBuilder.AppendFormat("{0,-20}{1}\n", reader.GetName(i), reader.GetValue(i));
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
