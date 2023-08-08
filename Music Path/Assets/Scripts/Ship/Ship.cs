using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ship : MonoBehaviour
{
    [Header("Move Animation")]
    public float moveDuration = .3f;
    public Ease moveEase = Ease.OutBack;
    public Action OnDie;
    public Action OnTrigger;
    public float edgeMove = .2f;
    private int _currentNote = 4;
    private bool _inTutorialMode = false;

    void Awake()
    {
        GameManager.Instance.SetPlayerShip(this);
        TouchManager.Instance.OnMoveSwipe += Move;
        _currentNote = 4;
        transform.position = new Vector2(transform.position.x, NotesManager.Instance.notes[_currentNote].transform.position.y);
    }

    public void Move(int direction)
    {
        int note = _currentNote + direction;
        if(note >= 0 && note < NotesManager.Instance.notes.Count)
        {
            ChangeNote(note);
        }
        else
        {
            transform.DOKill();
            transform.DOMoveY(edgeMove * direction, moveDuration/2).SetRelative().OnComplete(
                () => transform.DOMoveY(NotesManager.Instance.notes[_currentNote].transform.position.y, moveDuration/2).SetEase(moveEase)
            );
        }
    }

    private void ChangeNote(int note)
    {
        _currentNote = note;
        NotesManager.Instance.notes[_currentNote].PlayNote();
        transform.DOKill();
        transform.DOMoveY(NotesManager.Instance.notes[_currentNote].transform.position.y, moveDuration).SetEase(moveEase);
    }

    public int GetCurrentNote()
    {
        return _currentNote;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!_inTutorialMode)
        {
            Die();
        }
        else
        {
            OnTrigger?.Invoke();
        }
    }

    private void Die()
    {
        transform.DOScale(Vector3.zero, .3f).SetEase(Ease.InBounce);
        OnDie?.Invoke();
    }

    public void TutorialMode()
    {
        _inTutorialMode = true;
        DisableMove();
    }

    public void EndTutorialMode()
    {
        _inTutorialMode = false;
        EnableMove();
    }

    public void EnableMove()
    {   
        TouchManager.Instance.OnMoveSwipe += Move;
    }

    public void DisableMove()
    {
        TouchManager.Instance.OnMoveSwipe -= Move;
    }

    public bool InTutorialMode()
    {
        return _inTutorialMode;
    }
}
