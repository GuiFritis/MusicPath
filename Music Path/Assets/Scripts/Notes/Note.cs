using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public AudioClip noteAudio;

    public void PlayNote()
    {
        
    }

    public void SetAudio(AudioClip audio)
    {
        noteAudio = audio;
    }
}
