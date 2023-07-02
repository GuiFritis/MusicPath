using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using Sounds;

public class Melody : Singleton<Melody>
{
    public SOInt score;
    public VolumeChangeHelper volumeChangeHelper;
    [Tooltip("Volume at which player's note will get when melody is playing")]
    public float targetVolume = -20f;
    [Space]
    public Ship ship;
    [Space]
    public float startSpeed = 4f;
    private float _currentSpeed = 4f;
    private float _timer = 0f;
    public float speedUp = .1f;
    public float minSpeed = 1f;
    [Tooltip("Time between notes playing")]
    public float rhythim = .2f;
    private bool _playing = false;
    private int _lastNote;
    [Header("Sliding Notes")]
    [Tooltip("Delay added per note")]
    public float slideDelay = .05f;
    public float spawnX = 14f;
    public float slidingSpeed = 8f;
    public float slidingSpeedUp = .2f;
    public float maxSlidingSpeed = 40f;
    private float _currentSlidingSpeed = 8f;

    void Start()
    {
        score.Value = 0;
        _currentSpeed = startSpeed;
        _currentSlidingSpeed = slidingSpeed;
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
        _lastNote = ship.GetCurrentNote();
        int noteIndex = Random.Range(0, NotesManager.Instance.notes.Count);
        while(Mathf.Abs(noteIndex - _lastNote) <= 1)
        {
            noteIndex = Random.Range(0, NotesManager.Instance.notes.Count);
        }
        volumeChangeHelper.ChangeMixerVolume(targetVolume, rhythim, Mathf.Abs(_lastNote - noteIndex) * rhythim);
        StartCoroutine(PlayMelody(noteIndex));
    }

    private IEnumerator PlayMelody(int noteIndex)
    {   
        float notesAmount = 0f;
        while (_lastNote != noteIndex)
        {
            _lastNote += (_lastNote - noteIndex > 0 ? -1 : 1);
            AudioPool.Instance.Play(NotesManager.Instance.notes[_lastNote].GetNoteAudioClip());
            notesAmount++;
            yield return new WaitForSeconds(rhythim);
        }
        yield return new WaitForSeconds(notesAmount * slideDelay);
        SlideNotes();
    }

    private void SlideNotes()
    {
        for (int i = 0; i < NotesManager.Instance.notes.Count; i ++)
        {
            if(i != _lastNote)
            {
                SlidingNotePool.Instance.SlideNote(
                    new Vector2(spawnX, NotesManager.Instance.notes[i].transform.position.y),
                    _currentSlidingSpeed
                );
            }
        }
        score.Value++;
        _playing = false;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(spawnX, 10f), new Vector2(spawnX, -10f));
    }
}
