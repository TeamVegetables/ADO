﻿using System;
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
                new First(connection),
                new Second(connection),
                new Third(connection),
                new Sixth(connection),
                new Eleventh(connection),
                new Twelfth(connection),
                new Sixteenth(connection),
                new Nineteenth(connection),
                new Twentieth(connection),
                new TwentyFirst(connection),
                new TwentySecond(connection),
                new TwentyThird(connection),
                new TwentySixth(connection),
                new ThirtyFirst(connection),
                new ThirtySecond(connection)
            };
            foreach (var query in queries)
            {
                Console.Clear();
                Console.WriteLine(query.Title);
                Console.WriteLine(query.Execute());
                Console.ReadKey();
            }

            connection.Dispose();
        }
    }
}
