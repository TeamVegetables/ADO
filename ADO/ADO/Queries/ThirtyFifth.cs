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
            command.CommandText = "DELETE FROM Employees WHERE LastName = 'John' AND FirstName = 'Doe' AND Notes LIKE 'Adam';";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        stringBuilder.AppendFormat("Deleted {0} rows\n", command.ExecuteNonQuery());
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
