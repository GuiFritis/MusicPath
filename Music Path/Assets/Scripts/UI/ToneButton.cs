using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToneButton : MonoBehaviour
{
    public TextMeshProUGUI toneText;

    void Start()
    {
        toneText.text = GameManager.Instance.GetToneName();
    }

    public void ChangeTone()
    {
        GameManager.Instance.IncreaseTone();
        toneText.text = GameManager.Instance.GetToneName();
    }
}
