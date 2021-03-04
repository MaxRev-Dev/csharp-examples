using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;

namespace csharp_dev
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // problem: sequential output e.g aaabbbccc
            // task: merge thread output to vary sequence: abcabc
            await SyncSolution();
            Console.WriteLine();
            await AsyncSolution();
            // just block until input
            Console.Read();
        }

        private static async Task SyncSolution()
        {
            using var mutex = new Mutex();
            mutex.WaitOne(); // block others and wait for all threads to start
            var tasks = new List<Task>();
            var repeatSequenceFor = 3;
            // run all threads
            for (char i = 'a'; i <= 'd'; i++)
            {
                var threadData = i; // copy current data for the thread beyond
                tasks.Add(PrintTask(threadData, repeatSequenceFor, mutex));
            }

            // start work
            mutex.ReleaseMutex();
            await Task.WhenAll(tasks);
        }

        private static Task AsyncSolution()
        {
            var repeatSequenceFor = 3;

            var tasks = new List<Task>();
            // run all threads
            for (char i = 'a'; i <= 'd'; i++)
            {
                var threadData = i; // copy current data for the thread beyond
                tasks.Add(PrintTask(threadData, repeatSequenceFor));
            }
            return Task.WhenAll(tasks);
        }
        private static async Task PrintTask(char v, int count)
        {
            await Task.Yield();
            while (count-- > 0)
            {
                await DoWork(v);
            }
        }
        private static async Task PrintTask(char v, int count, Mutex writeSync)
        {
            await Task.Yield();
            while (count-- > 0)
            {
                writeSync.WaitOne();
                DoWork(v).Wait(); // syncronize mutex thread
                writeSync.ReleaseMutex();
            }
        }

        private static async Task DoWork(char v)
        {
            // perform some work 
            await Task.Yield();
            Console.Write(v);
        }
    }
}
