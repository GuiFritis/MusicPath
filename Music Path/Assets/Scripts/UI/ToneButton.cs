using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToneButton : MonoBehaviour
{
    public TextMeshProUGUI toneText;
    public AudioSource audioSrc;

    void Start()
    {
        toneText.text = GameManager.Instance.GetToneName();
    }

    public void ChangeTone()
    {
        audioSrc.Stop();
        GameManager.Instance.IncreaseTone();
        toneText.text = GameManager.Instance.GetToneName();
        audioSrc.clip = GameManager.Instance.GetTone().mainNote;
        audioSrc.Play();
    }
}
