using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LifeWithoutTasks.Helper;

class Program
{
    static void Main()
    {
        Console.WriteLine("Task with CT");
        RunTasksWithCancellation();
        
        Thread.Sleep(500);
        
        Console.WriteLine("\nThreads");
        RunThreads();
        
        Thread.Sleep(500);
        
        
        Console.WriteLine("\nThreadPool");
        RunThreadPool();
        
        Thread.Sleep(500);


        Console.WriteLine("\nFinished.");
    }
    
    static void RunTasksWithCancellation()
    {
        using var cts = new CancellationTokenSource();
        var token = cts.Token;

        Task[] tasks = new Task[5];
        cts.CancelAfter(100);
        for (int i = 0; i < tasks.Length; i++)
        {
            int id = i;
            tasks[i] = Task.Run(() => MethodHelper.Md5Loop($"Task-{id}", token), token);
        }

        try
        {
            Task.WaitAll(tasks);
        }
        catch (AggregateException)
        {
            
        }
    }
    
    static void RunThreads()
    {
        bool stop = false;
        Thread[] threads = new Thread[5];

        for (int i = 0; i < threads.Length; i++)
        {
            int id = i;
            threads[i] = new Thread(() => MethodHelper.Md5LoopNoCT($"Thread-{id}", () => stop));
            threads[i].Start();
        }

        Thread.Sleep(100);
        stop = true;
        
    }
    static void RunThreadPool()
    {
        bool stop = false;

        for (int i = 0; i < 5; i++)
        {
            
            int id = i;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                MethodHelper.Md5LoopNoCT($"ThreadPool-{id}", () => stop);
                
            });
        }

        Thread.Sleep(100);
        stop = true;
    }
    
}
