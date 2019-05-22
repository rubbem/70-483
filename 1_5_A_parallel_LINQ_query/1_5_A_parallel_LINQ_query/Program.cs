using System;
using System.Linq;

namespace _1_5_A_parallel_LINQ_query
{
    class Program
    {
        class Person
        {
            public string Name { get; set; }
            public string City { get; set; }
        }

        public static bool CheckCity(string name)
        {
            if (name == "")
                throw new ArgumentException(name);
            return name == "Seattle";
        }
        static void Main(string[] args)
        {
            Person[] people = new Person[] {
                new Person { Name = "Alan", City = "Hull" },
                new Person { Name = "Beryl", City = "Seattle" },
                new Person { Name = "Charles", City = "London" },
                new Person { Name = "David", City = "Seattle" },
                new Person { Name = "Eddy", City = "Paris" },
                new Person { Name = "Fred", City = "Berlin" },
                new Person { Name = "Gordon", City = "Hull" },
                new Person { Name = "Henry", City = "Seattle" },
                new Person { Name = "Isaac", City = "Seattle" },
                new Person { Name = "James", City = "London" }};

            //var result = from person in
            //                people.AsParallel().AsOrdered()
            //                where person.City == "Seattle"
            //                select person;
            //var result = (from person in people.AsParallel()
            //              where person.City == "Seattle"
            //              orderby (person.Name)
            //              select new
            //              {
            //                  Name = person.Name
            //              }).AsSequential().Take(4);

            try
            {
                var result = from person in
                    people.AsParallel()
                             where CheckCity(person.City)
                             select person;
                result.ForAll(person => Console.WriteLine(person.Name));
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.InnerExceptions.Count + " exceptions.");
            }

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }
    }
}
