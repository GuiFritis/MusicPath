using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public List<TutorialStep> tutorialSteps = new();
        public float tutorialSlidingSpeed = 5f;
        public Ship ship;

        private int _currentStep = 0;
        private string _tutorialStrPref = "_TutorialMode";

        void Start()
        {
            if(PlayerPrefs.GetInt(_tutorialStrPref, -1) <= 0)
            {
                MelodyManager.Instance.TutorialMode(tutorialSlidingSpeed);
                TouchManager.Instance.OnTouchStart += NextStep;
                tutorialSteps[_currentStep].StartStep(this);
                ship.TutorialMode();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void NextStep()
        {
            if(tutorialSteps[_currentStep].CheckStepCompleted())
            {
                tutorialSteps[_currentStep].EndStep();
                _currentStep++;

                if(_currentStep < tutorialSteps.Count)
                {
                    tutorialSteps[_currentStep].StartStep(this);
                }
                else
                {
                    PlayerPrefs.SetInt(_tutorialStrPref, 1);
                    TouchManager.Instance.OnTouchStart -= NextStep;
                    MelodyManager.Instance.EndTutorialMode();
                    ship.EndTutorialMode();
                    gameObject.SetActive(false);
                }
            }
        }

        private void NextStep(Vector3 touchPos)
        {
            NextStep();
        }
    }
}