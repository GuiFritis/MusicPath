using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using Padrao.Core.Utils;

public class Melody : Singleton<Melody>
{
    public float startSpeed = 4f;
    private float _currentSpeed = 4f;
    private float _timer = 0f;
    public float speedUp = .1f;
    public float minSpeed = 1f;
    [Tooltip("Time between notes playing")]
    public float rhythim = .2f;
    private bool _playing = false;
    private int _lastNote = 4;
    [Header("Sliding Notes")]
    public SlidingNote slidingNotes_PFB;
    public float spawnX = 14f;
    public float slidingSpeed = 8f;
    public float slidingSpeedUp = .2f;
    public float maxSlidingSpeed = 40f;
    private float _currentSlidingSpeed = 8f;

    void Start()
    {
        _currentSpeed = startSpeed;
        _currentSlidingSpeed = slidingSpeed;
        _lastNote = 4;
    }

    void Update()
    {
        if(!_playing)
        {
            _timer += Time.deltaTime;
            if(_timer >= _currentSpeed)
            {
                _timer = 0f;
                _playing = true;
                PreviewMelody();
                if(_currentSpeed > minSpeed)
                {
                    _currentSpeed -= speedUp;
                }
                if(_currentSlidingSpeed < maxSlidingSpeed)
                {
                    _currentSlidingSpeed += slidingSpeedUp;
                }
            }
        }
    }

    private void PreviewMelody()
    {
        int noteIndex = Random.Range(0, NotesManager.Instance.notes.Count);
        while(noteIndex == _lastNote)
        {
            noteIndex = Random.Range(0, NotesManager.Instance.notes.Count);
        }
        StartCoroutine(PlayMelody(noteIndex));
    }

    private IEnumerator PlayMelody(int noteIndex)
    {        
        while (_lastNote != noteIndex)
        {
            _lastNote += (_lastNote - noteIndex > 0 ? -1 : 1);
            NotesManager.Instance.notes[_lastNote].PlayNote();
            yield return new WaitForSeconds(rhythim);
        }
        SlideNotes();
    }

    private void SlideNotes()
    {
        for (int i = 0; i < NotesManager.Instance.notes.Count; i ++)
        {
            if(i != _lastNote)
            {
                var slidingNote = Instantiate(
                    slidingNotes_PFB, 
                    new Vector2(spawnX, NotesManager.Instance.notes[i].transform.position.y), 
                    Quaternion.identity
                );
                slidingNote.speed = _currentSlidingSpeed;
                slidingNote.xDyingPoint = -spawnX;
            }
        }
        _playing = false;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(spawnX, 10f), new Vector2(spawnX, -10f));
    }
}
