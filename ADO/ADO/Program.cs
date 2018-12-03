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
                new First(connection),
                new Second(connection),
                new Third(connection),
                new Fourth(connection),
                new Fifth(connection),
                new Sixth(connection),
                new Seventh(connection),
                new Eigth(connection),
                new Ninth(connection),
                new Tenth(connection),
                new Eleventh(connection),
                new Twelfth(connection),
                new Thirteenth(connection),
                new Fourteenth(connection),
                new Fifteenth(connection),
                new Sixteenth(connection),
                new Seventeenth(connection),
                new Eighteenth(connection),
                new Nineteenth(connection),
                new Twentieth(connection),
                new TwentyFirst(connection),
                new TwentySecond(connection),
                new TwentyThird(connection),
                new TwentyFourth(connection),
                new TwentyFifth(connection),
                new TwentySixth(connection),
                new TwentySeventh(connection),
                new TwentyEighth(connection),
                new TwentyNinth(connection),
                new Thirtieth(connection),
                new ThirtyFirst(connection),
                new ThirtySecond(connection),
                new ThirtyThird(connection),
                new ThirtyFourth(connection),
                new ThirtyFifth(connection)

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
