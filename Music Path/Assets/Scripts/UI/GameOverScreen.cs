using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Save;

public class GameOverScreen : MonoBehaviour
{
    public AudioSource gameOverAudio;
    public TextMeshProUGUI scoreText;
    public SOInt score;
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

    public void GameOver()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    private IEnumerator ShowGameOverScreen()
    {

        panel.DOColor(_panelColor, duration);
        yield return new WaitForSeconds(duration);
        
        gameOverAudio?.Play();

        scoreText.text = (score.Value).ToString();
        scoreBox.transform.DOScale(_scoreBoxScale, duration).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(duration);

        bool newHighscore = score.Value > Save.SaveManager.Instance.GetHighscore();
        if(newHighscore)
        {
            Save.SaveManager.Instance.NewHighscore(score.Value);
            scoreBox.transform.DOMoveY(scoreBoxOffset, duration).SetRelative(true);
            highscore.gameObject.SetActive(true);
            highscore.transform.DOScale(_highscoreScale, duration).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(duration);
        }

        playButton.transform.DOScale(_playBtnScale, duration).SetEase(Ease.OutBounce);
        toneButton.transform.DOScale(_toneBtnScale, duration).SetEase(Ease.OutBounce);
    }
}
