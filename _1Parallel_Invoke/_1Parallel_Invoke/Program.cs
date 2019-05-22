using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _1Parallel_Invoke
{
    class Program
    {
        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: " + item); 
            Thread.Sleep(100);
            Console.WriteLine("Finished working on: " + item);
        }
        static void Main(string[] args)
        {
            var items = Enumerable.Range(0, 50);
            Parallel.ForEach(items, item =>
            {
                WorkOnItem(item);
            });

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();

            var items2 = Enumerable.Range(0, 50).ToArray();

            ParallelLoopResult result = Parallel.For(0, items2.Count(), (int i, ParallelLoopState loopState) =>
            {
                if (i == 20)
                    loopState.Break();

                WorkOnItem(items2[i]);
            });

            Console.WriteLine("Completed: " + result.IsCompleted);
            Console.WriteLine("Items: " + result.LowestBreakIteration);

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }
    }
}
