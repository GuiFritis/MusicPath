using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingNotePool : PoolBase<SlidingNote, SlidingNotePool>
{
    protected override void Awake()
    {
        base.Awake();
        foreach (var item in _pool)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void SlideNote(Vector2 pos, float speed)
    {
        var item = GetPoolItem();
        item.transform.position = pos;        
        item.speed = speed;
        item.gameObject.SetActive(true);
    }
}
