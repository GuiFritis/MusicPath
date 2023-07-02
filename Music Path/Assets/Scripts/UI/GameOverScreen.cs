using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameOverScreen : MonoBehaviour
{
    public float duration = .3f;
    public Image panel;
    private Color _panelColor;
    public GameObject scoreBox;
    private Vector3 _scoreBoxScale;
    public GameObject highscore;
    public float scoreBoxOffset = 1f;
    private Vector3 _highscoreScale;
    public GameObject playButton;
    private Vector3 _playBtnScale;
    public GameObject toneButton;
    private Vector3 _toneBtnScale;

    void Awake()
    {
        GameManager.Instance.SetGameOverScreen(this);

        _panelColor = panel.color;
        panel.color = Color.clear;

        _scoreBoxScale = scoreBox.transform.localScale;
        scoreBox.transform.localScale = Vector3.zero;
        
        _highscoreScale = highscore.transform.localScale;
        highscore.gameObject.SetActive(false);
        highscore.transform.localScale = Vector3.zero;

        _playBtnScale = playButton.transform.localScale;
        playButton.transform.localScale = Vector3.zero;

        _toneBtnScale = toneButton.transform.localScale;
        toneButton.transform.localScale = Vector3.zero;
    }

    public void GameOver(bool newHighscore = false)
    {
        StartCoroutine(ShowGameOverScreen(newHighscore));
    }

    private IEnumerator ShowGameOverScreen(bool newHighscore)
    {
        panel.DOColor(_panelColor, duration);
        yield return new WaitForSeconds(duration);
        scoreBox.transform.DOScale(_scoreBoxScale, duration).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(duration);
        if(newHighscore)
        {
            scoreBox.transform.DOMoveY(scoreBoxOffset, duration).SetRelative(true);
            highscore.gameObject.SetActive(true);
            highscore.transform.DOScale(_highscoreScale, duration).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(duration);
        }
        playButton.transform.DOScale(_playBtnScale, duration).SetEase(Ease.OutBounce);
        toneButton.transform.DOScale(_toneBtnScale, duration).SetEase(Ease.OutBounce);
    }
}
