using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class ThirtyThird : IQuery
    {
        private readonly IDbConnection _connection;

        public ThirtyThird(IDbConnection connection)
        {
            _connection = connection;
            Title = "Change the City field in one of your records using the UPDATE statement";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = "UPDATE Employees " +
                                  "SET City = 'Lexington' " +
                                  "WHERE LastName = 'Mike' AND Notes LIKE 'Adam';";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("UPDATED WHERE LastName = 'Mike' AND Notes LIKE 'Adam'\n");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
