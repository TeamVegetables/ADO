using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class First : IQuery
    {
        private readonly IDbConnection _connection;

        public First(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show all info about the employee with ID 8";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT * FROM Employees WHERE EmployeeID = 8;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("{0,-20}{1}", reader.GetName(i), reader.GetValue(i));
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
