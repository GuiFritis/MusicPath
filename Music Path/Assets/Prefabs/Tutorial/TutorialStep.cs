using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialStep : MonoBehaviour
    {
        [Min(0.3f)]
        public float dismissTime = 1f;

        protected float _timer = 0f;

        protected virtual void Update()
        {
            _timer += Time.deltaTime;
        }

        public virtual void StartStep(TutorialManager manager)
        {
            gameObject.SetActive(true);
        }

        public virtual void EndStep()
        {
            gameObject.SetActive(false);
        }

        public virtual bool CheckStepCompleted()
        {
            return _timer >= dismissTime;
        }
    }
}
