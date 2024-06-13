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
            _tutorialManager.ship.EnableMove();
            TouchManager.Instance.OnMoveSwipe += CheckSwipe;
        }

        public override void EndStep()
        {
            base.EndStep();
            _tutorialManager.ship.DisableMove();
            TouchManager.Instance.OnMoveSwipe -= CheckSwipe;
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
                _tutorialManager.NextStep();
            }
        }
    }
}