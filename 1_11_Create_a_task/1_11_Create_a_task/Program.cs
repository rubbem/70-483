using System;
using System.Threading;
using System.Threading.Tasks;

namespace _1_11_Create_a_task
{
    class Program
    {
        public static void DoWork(int i)
        {
            Console.WriteLine("Task {0} starting", i);
            Thread.Sleep(2000);
            Console.WriteLine("Task {0} finished", i);
        }

        public static int CalculateResult()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
            return 99;
        }

        public static void HelloTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Hello");
        }

        public static void WorldTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("World");
        }

        public static void DoChild(object state)
        {
            Console.WriteLine("Child {0} starting", state);
            Thread.Sleep(2000);
            Console.WriteLine("Child {0} finished", state);
        }
        static void Main(string[] args)
        {
            var parent = Task.Factory.StartNew(() => {
                Console.WriteLine("Parent starts");
                for (int i = 0; i < 10; i++)
                {
                    int taskNo = i;
                    Task.Factory.StartNew(
                        (x) => DoChild(x), // lambda expression
                               taskNo, // state object
                               TaskCreationOptions.AttachedToParent);
                }
            });

            parent.Wait(); // will wait for all the attached children to complete

            Console.WriteLine("Parent finished. Press a key to end.");
            Console.ReadKey();

            Task[] Tasks = new Task[10];

            for (int i = 0; i < 10; i++)
            {
                int taskNum = i;  // make a local copy of the loop counter so that the 
                                  // correct task number is passed into the 
                //                  lambda expression
                Tasks[i] = Task.Run(() => DoWork(taskNum));
            }
            Task.WaitAll(Tasks);

            Console.WriteLine("Finished processing. Press a key to end.");

            Task task2 = Task.Run(() => HelloTask());

            task2.ContinueWith((prevTask) => WorldTask(), TaskContinuationOptions.OnlyOnRanToCompletion);
            task2.ContinueWith((prevTask) => ExceptionTask(), TaskContinuationOptions.OnlyOnFaulted);

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();

            Console.ReadKey();

            Task<int> task = Task.Run(() =>
            {
                return CalculateResult();
            });

            Console.WriteLine(task.Result);

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }

        private static void ExceptionTask()
        {
            throw new NotImplementedException();
        }
    }
}
