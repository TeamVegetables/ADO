using System.Data;
using System.Text;
using ADO.Interfaces;


namespace ADO.Queries
{
    public class ThirtyFifth:IQuery
    {

        private readonly IDbConnection _connection;

        public ThirtyFifth(IDbConnection connection)
        {
            _connection = connection;
            Title = "Delete one of your records";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "DELETE FROM Employees WHERE LastName = 'Derek' AND FirstName = 'Lunsford';";

            stringBuilder.AppendFormat("Deleted {0} rows\n", command.ExecuteNonQuery());

            return stringBuilder.ToString();
        }
    }
}
