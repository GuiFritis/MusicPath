using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;
using TMPro;

public class TextHighscore : MonoBehaviour
{
    public string previousText = "Highscore ";
    public TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh.text = previousText + SaveManager.Instance.GetHighscore();
    }

}
