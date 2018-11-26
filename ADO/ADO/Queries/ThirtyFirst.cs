using System;
using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class ThirtyFirst : IQuery
    {
        private readonly IDbConnection _connection;

        public ThirtyFirst(IDbConnection connection)
        {
            _connection = connection;
            Title = "Insert 5 new records into Employees table."
                    + "Fill in the following  fields: LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes."
                    + "The Notes field should contain your own name";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();

            command.CommandText = String.Concat(
                "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) ",
                "VALUES ",
                "('John', 'Cena', '1990-01-01', '2000-01-01', 'Baker Street 221B', 'London', 'United Kingdom', 'Adam'), ",
                "('Bill', 'Fred', '1990-01-01', '2000-01-01', 'Baker Street 221B', 'London', 'United Kingdom', 'Adam'), ",
                "('Mikle', 'Fors', '1990-01-01', '2000-01-01', 'Baker Street 221B', 'London', 'United Kingdom', 'Adam'), ",
                "('Cris', 'Djeriko', '1990-01-01', '2000-01-01', 'Baker Street 221B', 'London', 'United Kingdom', 'Adam'), ",
                "('Derek', 'Lunsford', '1990-01-01', '2000-01-01', 'Baker Street 221B', 'London', 'United Kingdom', 'Adam');");

            stringBuilder.AppendFormat($"Inserted {command.ExecuteNonQuery()} rows");

            return stringBuilder.ToString();
        }
    }
}
