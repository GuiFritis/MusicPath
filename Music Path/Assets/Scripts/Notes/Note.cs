using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sounds;

public class Note : MonoBehaviour
{
    public AudioSource noteAudio;

    public void PlayNote()
    {
        if(noteAudio != null)
        {
            noteAudio.Play();
        }
    }

    public AudioClip GetNoteAudioClip()
    {
        return noteAudio.clip;
    }

    public void SetAudioClip(AudioClip audio)
    {
        noteAudio.clip = audio;
    }
}
