using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tutorial
{
    public class TutorialMelody : TutorialStep
    {
        public ScoreCounter scoreCounter;
        public TextMeshProUGUI stepTextMesh;
        public List<string> stepTexts = new();
        public TextMeshProUGUI nextStepTextMesh;
        public List<string> nextStepTexts = new();

        private int _tries = 0;
        private TutorialManager _tutorialManager;

        public override void StartStep(TutorialManager manager)
        {
            base.StartStep(manager);
            MelodyManager.Instance.PreviewMelody();
            MelodyManager.Instance.score.OnValueChanged += Success;
            _tutorialManager = manager;
            _tutorialManager.ship.EnableMove();
            _tutorialManager.ship.OnTrigger += Fail;
        }

        public override void EndStep()
        {
            base.EndStep();
            MelodyManager.Instance.score.OnValueChanged -= Success;
            _tutorialManager.ship.OnTrigger -= Fail;
            _tutorialManager.ship.DisableMove();
        }

        public override bool CheckStepCompleted()
        {
            return MelodyManager.Instance.score.Value > 0;
        }

        public void Fail()
        {
            scoreCounter.gameObject.SetActive(false);
            _tries++;
            if(_tries < stepTexts.Count)
            {
                stepTextMesh.text = stepTexts[_tries];
            }
            if(_tries < nextStepTexts.Count)
            {
                nextStepTextMesh.text = nextStepTexts[_tries];
            }
            MelodyManager.Instance.PreviewMelody();
            Invoke(nameof(EnableScoreCounter), 1f);
        }

        private void EnableScoreCounter()
        {
            scoreCounter.gameObject.SetActive(true);
        }

        private void Success(int score)
        {
            _tutorialManager.NextStep();
        }
    }
}
