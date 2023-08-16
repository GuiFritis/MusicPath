using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tone")]
public class SO_Tone : ScriptableObject
{
    public AudioClip mainNote;
    public List<AudioClip> notes;
}
