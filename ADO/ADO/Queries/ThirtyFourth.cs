using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class ThirtyFourth:IQuery
    {
        private readonly IDbConnection _connection;

        public ThirtyFourth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Change the HireDate field in all your records to current date";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "UPDATE Employees " +
                                  "SET HireDate = '2018-11-18' " +
                                  "WHERE Notes LIKE 'Adam';";
    
            stringBuilder.AppendFormat($"\nUpdate {command.ExecuteNonQuery()} row");

            return stringBuilder.ToString();
        }
    }
}
