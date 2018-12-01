using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Fifth : IQuery
    {
        private readonly IDbConnection _connection;

        public Fifth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Calculate the count of employees from London";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT COUNT(*) AS EmployeesAmount FROM Employees WHERE City = 'London';";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   stringBuilder.Append(reader["EmployeesAmount"]+"\n");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
