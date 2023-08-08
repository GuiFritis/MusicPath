using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public Ship ship;
    public ParticleSystem scoreVfx;
    public float rayDistance = 0.5f;
    public float offsetX = -1.5f;
    public LayerMask slidingNoteLayer;
    public float waitingTime = .7f;
    private float _timer = 0f;

    void Start()
    {
        ship.OnDie += () => gameObject.SetActive(false);
    }
    
    void FixedUpdate()
    {
        if(_timer > waitingTime)
        {
            var ray = Physics2D.Linecast(
                transform.position + Vector3.up * rayDistance + Vector3.right * offsetX, 
                transform.position + Vector3.down * rayDistance + Vector3.right * offsetX, 
                slidingNoteLayer
            );

            if(ray.collider != null)
            {
                MelodyManager.Instance.PointScored();
                scoreVfx?.Play();
                _timer = 0f;   
            }
        }
        else
        {
            _timer += Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(
            transform.position + Vector3.up * rayDistance + Vector3.right * offsetX, 
            transform.position + Vector3.down * rayDistance + Vector3.right * offsetX
        );
    }
}
