using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScore : MonoBehaviour
{
    public TextMeshProUGUI text;
    public SOInt score;

    void Awake()
    {
        score.OnValueChanged += OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        text.text = $"Score: {value}";
    }
}
