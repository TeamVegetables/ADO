﻿using System.Data;
using System.Text;
using ADO.Interfaces;

namespace ADO.Queries
{
    public class TwentySecond : IQuery
    {
        private readonly IDbConnection _connection;

        public TwentySecond(IDbConnection connection)
        {
            _connection = connection;
            Title = "Show the list of french customers’ names who used to order non-french products (use left join).";
        }

        public string Title { get; }

        public string Execute()
        {
            var command = _connection.CreateCommand();
            var stringBuilder = new StringBuilder();
            command.CommandText = string.Concat("SELECT DISTINCT C.ContactName ",
                "FROM Customers as C ",
                "LEFT JOIN Orders AS O ON C.CustomerID = O.CustomerID ",
                "LEFT JOIN [Order Details] AS OD ON O.OrderID = OD.OrderID ",
                "LEFT JOIN [Products] AS P ON OD.ProductID = P.ProductID ",
                "LEFT JOIN [Suppliers] AS S ON P.SupplierID = S.SupplierID ",
                "WHERE S.Country <> 'France' AND C.Country = 'France'");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stringBuilder.AppendFormat("{0}\n", reader["ContactName"]);
                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }
    }
}
