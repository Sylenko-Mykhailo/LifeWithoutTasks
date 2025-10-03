namespace TSList;

using System;
using System.Collections.Generic;

public class ThreadSafeList<T>
{
    private readonly List<T> _list = new();
    private readonly object _lock = new();

    public void Add(T item)
    {
        lock (_lock)
        {
            _list.Add(item);
        }
    }

    public bool Remove(T item)
    {
        lock (_lock)
        {
            return _list.Remove(item);
        }
    }

    public T this[int index]
    {
        get
        {
            lock (_lock)
            {
                return _list[index];
            }
        }
        set
        {
            lock (_lock)
            {
                _list[index] = value;
            }
        }
    }

    public int Count
    {
        get
        {
            lock (_lock)
            {
                return _list.Count;
            }
        }
    }

    public T[] ToArray()
    {
        lock (_lock)
        {
            return _list.ToArray();
        }
    }
}
