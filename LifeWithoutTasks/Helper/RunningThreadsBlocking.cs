namespace LifeWithoutTasks.Helper;

public static class RunningThreadsBlocking
{
    public static void RunThreads()
    {
        bool stop = false;
        Thread[] threads = new Thread[5];

        for (int i = 0; i < threads.Length; i++)
        {
            int id = i;
            threads[i] = new Thread(() => MethodHelper.Md5LoopNoCT($"Thread-{id}", () => stop));
            threads[i].Start();
        }

        Thread.Sleep(500);
        stop = true;

        foreach (var t in threads)
        {
            t.Join();
        }
    }
    public static void RunThreadPool()
    {
        bool stop = false;
        CountdownEvent countdown = new CountdownEvent(5);

        for (int i = 0; i < 5; i++)
        {
            
            int id = i;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                MethodHelper.Md5LoopNoCT($"ThreadPool-{id}", () => stop);
                countdown.Signal();
            });
        }

        Thread.Sleep(500);
        stop = true;
        countdown.Wait();
    }
}