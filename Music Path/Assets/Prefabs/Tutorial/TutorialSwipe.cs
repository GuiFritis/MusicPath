using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialSwipe : TutorialStep
    {
        [Range(-1, 1)]
        public int swipeDirection = 1;

        private TutorialManager _tutorialManager;
        private bool _swiped = false;

        public override void StartStep(TutorialManager manager)
        {
            base.StartStep(manager);
            _tutorialManager = manager;
            TouchManager.Instance.OnMoveSwipe += CheckSwipe;
        }

        public override bool CheckStepCompleted()
        {
            return _swiped;
        }

        public void CheckSwipe(int direction)
        {            
            if(direction == swipeDirection)
            {
                _swiped = true;
                TouchManager.Instance.OnMoveSwipe -= CheckSwipe;
                _tutorialManager.NextStep();
            }
        }
    }
}