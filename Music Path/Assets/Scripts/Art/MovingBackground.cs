using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Utils;
using Padrao.Core.Singleton;

public class MovingBackground : Singleton<MovingBackground>
{
    public List<MovingPiece> pieces = new();
    [Range(0f, 1f)]
    public float speedMultiplier = 0.7f;
    public float spawnPositionX = -15f;
    [Tooltip("Multiplies this with the sliding speed to maintain the distance between spawns")]
    public float spawnTime = 2f;

    private float _timer = 0f;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i] = Instantiate(pieces[i], transform);
            pieces[i].gameObject.SetActive(false);
        }
    }

    void Start()
    {   
        SlidingSpeedChanged(MelodyManager.Instance.GetCurrentSlidingSpeed());
        MelodyManager.Instance.OnSlidingSpeedChange += SlidingSpeedChanged;
    }

    void Update() 
    {
        if(_timer >= spawnTime)
        {
            _timer = 0f;
            SlidePiece();
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private MovingPiece GetPiece()
    {
        MovingPiece piece;
        for(int i = 0; i < 10; i++)
        {
            piece = pieces.GetRandom();
            if(!piece.gameObject.activeInHierarchy)
            {
                return piece;
            }
        }
        return pieces.Find(i => !i.gameObject.activeInHierarchy);
    }

    private void SlidePiece()
    {
        var item = GetPiece();
        if(item == null)
        {
            return;
        }
        item.transform.position = new Vector3(spawnPositionX, item.positionY);        
        item.speed = MelodyManager.Instance.GetCurrentSlidingSpeed() * speedMultiplier;
        item.xDyingPoint = -spawnPositionX;
        item.gameObject.SetActive(true);
    }

    private void SlidingSpeedChanged(float value)
    {
        foreach (var item in pieces)
        {
            item.speed = value * speedMultiplier;
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(spawnPositionX, 10f), new Vector3(spawnPositionX, -10f));
    }
}
