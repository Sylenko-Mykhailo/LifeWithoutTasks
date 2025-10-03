using System;
using System.Threading.Tasks;
using TSList;  // Use your namespace

class Program
{
    static async Task Main()
    {
        var safeList = new ThreadSafeList<int>();

        var addTasks = new Task[10];
        for (int i = 0; i < addTasks.Length; i++)
        {
            int taskNum = i;
            addTasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < 10; j++)
                {
                    safeList.Add(taskNum * 10 + j);
                }
            });
        }

        await Task.WhenAll(addTasks);

        Console.WriteLine($"Items after adding: {safeList.Count}");
        
        var removeTasks = new Task[6];
        for (int i = 0; i < removeTasks.Length; i++)
        {
            int taskNum = i;
            removeTasks[taskNum] = Task.Run(() =>
            {
                for (int j = 0; j < 10; j++)
                {
                    int toRemove = 10 * taskNum + j;
                    safeList.Remove(toRemove);
                }
            });
        }

        await Task.WhenAll(removeTasks);

        Console.WriteLine($"Items after removing: {safeList.Count}");

        var arr = safeList.ToArray();
        Console.WriteLine("First 10 items:");
        for (int i = 0; i < Math.Min(10, arr.Length); i++)
        {
            Console.WriteLine(arr[i]);
        }
    }
}