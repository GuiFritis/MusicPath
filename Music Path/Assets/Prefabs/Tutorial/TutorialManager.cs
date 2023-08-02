using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public List<TutorialStep> tutorialSteps = new();
        public float tutorialSlidingSpeed = 5f;
        public SOInt score;

        private int _currentStep = 0;

        void Start()
        {
            MelodyManager.Instance.TutorialMode(tutorialSlidingSpeed);
            TouchManager.Instance.OnTouchStart += NextStep;
            tutorialSteps[_currentStep].StartStep(this);
        }

        public void NextStep()
        {
            if(tutorialSteps[_currentStep].CheckStepCompleted())
            {
                tutorialSteps[_currentStep].EndStep();
                _currentStep++;
            }

            if(_currentStep < tutorialSteps.Count)
            {
                tutorialSteps[_currentStep].StartStep(this);
            }
            else
            {
                TouchManager.Instance.OnTouchStart -= NextStep;
                MelodyManager.Instance.EndTutorialMode();
                gameObject.SetActive(false);
            }
        }

        private void NextStep(Vector3 touchPos)
        {
            NextStep();
        }
    }
}