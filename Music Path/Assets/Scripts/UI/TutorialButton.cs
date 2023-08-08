using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    private string _tutorialStrPref = "_TutorialMode";

    void Start()
    {
        int tutorialMode = PlayerPrefs.GetInt(_tutorialStrPref, -1);
        if(tutorialMode == -1)
        {
            gameObject.SetActive(false);
        } 
        else if(tutorialMode == 0) 
        {
            PlayerPrefs.SetInt(_tutorialStrPref, 1);
        }
    }

    public void ResetTutorial()
    {
        PlayerPrefs.SetInt(_tutorialStrPref, 0);
        transform.DOScale(0f, .3f).SetEase(Ease.InBounce).OnComplete(() => {gameObject.SetActive(false); SceneManager.LoadScene(1);});
    }
}
