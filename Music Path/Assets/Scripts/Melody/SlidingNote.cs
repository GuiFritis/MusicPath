using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingNote : MonoBehaviour
{
    public float speed = 1f;
    public float xDyingPoint = -14f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if(transform.position.x < xDyingPoint)
        {
            gameObject.SetActive(false);
        }
    }
}
