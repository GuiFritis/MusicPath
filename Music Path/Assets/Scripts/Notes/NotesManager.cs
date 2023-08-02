using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

[DefaultExecutionOrder(-1)]
public class NotesManager : Singleton<NotesManager>
{
    public List<Note> notes = new();

    protected override void Awake()
    {
        base.Awake();
        var tone = GameManager.Instance.GetTone();
        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].SetAudioClip(tone.notes[i]);
        }
    }
}
