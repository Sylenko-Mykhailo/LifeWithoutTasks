using System.Security.Cryptography;
using System.Text;

namespace LifeWithoutTasks.Helper;

public static class MethodHelper
{
    
    public static void Md5LoopNoCT(string name, Func<bool> stopSignal)
    {
        using var md5 = MD5.Create();
        byte[] data = Encoding.UTF8.GetBytes("AAAAAAAAAAAABBBBBBBBBBBBCCCCCCCCCCDDDDDDDDDD");
        var counter = 0;

        while (!stopSignal())
        {
            data = md5.ComputeHash(data);
            counter++;
        }
        Console.WriteLine($"{name} stopped with count counter {counter}.");
    }
    public static void Md5Loop(string name, CancellationToken token)
    {
        using var md5 = MD5.Create();
        byte[] data = Encoding.UTF8.GetBytes("AAAAAAAAAAAABBBBBBBBBBBBCCCCCCCCCCDDDDDDDDDD");
        var counter = 0;
        try
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                data = md5.ComputeHash(data);
                counter++;
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"{name} stopped with count {counter}.");
        }
    }
}