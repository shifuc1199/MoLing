using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{

    private List<T> _list=new List<T>();
    public bool isEmpty{get{ return _list.Count == 0;}}
    public void Add(T temp)
    {
        _list.Add(temp);
    }
    public T Get()
    {
        if(!isEmpty)
        {
            T t = _list[0];
            _list.RemoveAt(0);
            return t;
        }
        else
        {
            return default(T);
        }
       
    }
    
}
