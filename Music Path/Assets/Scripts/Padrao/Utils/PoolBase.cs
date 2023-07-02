using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolBase<T, X> : MonoBehaviour where T : Behaviour where X : MonoBehaviour
{
    public static X Instance;
    public int preWarmSize = 2;
    public bool finite = false;
    public int maxPoolSize = 1000;
    public T PFB_item;

    protected List<T> _pool = new List<T>();
    protected int _currentIndex;

    protected virtual void Awake()
    {
        if(Instance == null){
            Instance = GetComponent<X>();
        } else {
            Destroy(gameObject);
        }
        InitPool();
    }

    private void InitPool()
    {
        _pool = new List<T>();
        _currentIndex = 0;

        for (int i = 0; i < preWarmSize; i++)
        {
            CreatePoolItem();
        }
    }

    private void CreatePoolItem()
    {
        _pool.Add(Instantiate(PFB_item, gameObject.transform));
    }

    public T GetPoolItem()
    {
        T item = null;
        if(finite)
        {
            if(_currentIndex == _pool.Count)
            {
                if(maxPoolSize == _pool.Count)
                {
                    _currentIndex = 0;
                }
                else
                {
                    CreatePoolItem();
                }
            }
            item = _pool[_currentIndex];
            _currentIndex++;
        }
        else
        {
            item = _pool.Find(CheckItem);
            if(item == null)
            {
                CreatePoolItem();
                item = _pool[^1];
            }
        }
        return item;
    }

    protected virtual bool CheckItem(T item)
    {
        return !item.gameObject.activeInHierarchy;
    }
}
