using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public List<Note> notes = new List<Note>();
    private int _currentNote = 5;

    void Awake()
    {
        TouchManager.Instance.OnMoveSwipe += Move;
    }

    public void Move(int direction)
    {
        int note = _currentNote + direction;
        if(note >= 0 && note < notes.Count)
        {
            ChangeNote(note);
        }
    }

    private void ChangeNote(int note)
    {
        _currentNote = note;
        notes[_currentNote].PlayNote();
        transform.position = notes[_currentNote].transform.position;
    }

    private int GetCurrentNote()
    {
        return _currentNote;
    }
}
