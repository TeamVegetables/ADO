using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using ADO.Interfaces;
using ADO.Queries;

namespace ADO
{
    internal class Program
    {
        private static void Main()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString);
            connection.Open();
            var queries = new List<IQuery>
            {
                new Second(connection)
            };
            foreach (var query in queries)
            {
                Console.WriteLine(query.Title);
                query.Execute();
            }

            Console.ReadKey();
            connection.Dispose();
        }
    }
}
