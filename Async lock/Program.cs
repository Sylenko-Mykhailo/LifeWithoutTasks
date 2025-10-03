using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static readonly SemaphoreSlim _lock = new(1, 1);

    static async Task Main()
    {
        var task1 = DoWorkAsync("Task 1");
        var task2 = DoWorkAsync("Task 2");
        await Task.WhenAll(task1, task2);
    }

    static async Task DoWorkAsync(string name)
    {
        await _lock.WaitAsync();
        try
        {
            Console.WriteLine($"{name} entered lock");
            await Task.Delay(2000);
            Console.WriteLine($"{name} leaving lock");
        }
        finally
        {
            _lock.Release();
        }
    }
}