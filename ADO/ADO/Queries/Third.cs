using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class Third : IQuery
    {
        private readonly IDbConnection _connection;

        public Third(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of first and last names of the employees whose first name begins with letter A";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "SELECT FirstName, LastName FROM Employees WHERE FirstName LIKE \'A%\';";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0}\t{1}\n", reader["FirstName"], reader["LastName"]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
