using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using Sounds;

public class MelodyManager : Singleton<MelodyManager>
{
    public SOInt score;
    public VolumeChangeHelper volumeChangeHelper;
    [Tooltip("Volume at which player's note will get when melody is playing")]
    public float targetVolume = -20f;
    [Space]
    [SerializeField]
    private Ship _ship;
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
    private int _currentNote;
    [Header("Sliding Notes")]
    [Tooltip("Delay added per note")]
    public float slideDelay = .05f;
    public float spawnX = 14f;
    public float slidingSpeed = 8f;
    public float slidingSpeedUp = .2f;
    public float maxSlidingSpeed = 40f;
    private float _currentSlidingSpeed = 8f;
    private bool _inTutorialMode = false;

    void Start()
    {
        score.Value = 0;
        _currentSpeed = startSpeed;
        _currentSlidingSpeed = slidingSpeed;
    }

    void Update()
    {
        if(!_playing && !_inTutorialMode)
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

    public void PreviewMelody()
    {
        _lastNote = _ship.GetCurrentNote();
        _currentNote = Random.Range(0, NotesManager.Instance.notes.Count);
        while(Mathf.Abs(_currentNote - _lastNote) <= 1)
        {
            _currentNote = Random.Range(0, NotesManager.Instance.notes.Count);
        }
        volumeChangeHelper.ChangeMixerVolume(targetVolume, rhythim, Mathf.Abs(_lastNote - _currentNote) * rhythim);
        StartCoroutine(PlayMelody(_lastNote, _currentNote, rhythim));
    }

    private IEnumerator PlayMelody(int currentNoteIndex, int targetNoteIndex, float timing)
    {   
        float notesAmount = 0f;
        int curNote = currentNoteIndex;
        while (curNote != targetNoteIndex)
        {
            curNote += curNote - targetNoteIndex > 0 ? -1 : 1;
            AudioPool.Instance.Play(NotesManager.Instance.notes[curNote].GetNoteAudioClip());
            notesAmount++;
            yield return new WaitForSeconds(timing);
        }
        yield return new WaitForSeconds(notesAmount * slideDelay);
        SlideNotes();
    }

    private void SlideNotes()
    {
        for (int i = 0; i < NotesManager.Instance.notes.Count; i ++)
        {
            if(i != _currentNote)
            {
                SlidingNotePool.Instance.SlideNote(
                    new Vector2(spawnX, NotesManager.Instance.notes[i].transform.position.y),
                    _currentSlidingSpeed
                );
            }
        }
        _playing = false;
    }

    public void PointScored()
    {
        score.Value++;
    }

    public void TutorialMode(float tutorialSlidingSpeed)
    {
        _inTutorialMode = true;
        _currentSlidingSpeed = tutorialSlidingSpeed;
    }

    public void EndTutorialMode()
    {
        _inTutorialMode = false;
        _currentSlidingSpeed = slidingSpeed;
        score.Value = 0;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(spawnX, 10f), new Vector2(spawnX, -10f));
    }
}
