using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Libs.Base.GameLogic
{
    public class RandomDelayEventBehaviour : MonoBehaviour
    {
        [SerializeField] float delayFrom = 1;
        [SerializeField] float delayTo = 2;
        [SerializeField] float currentDelay = -1;
        [SerializeField] int repeatCount = 1;
        [SerializeField] ConditionBehaviour[] conditions;

        public UnityEvent OnEventFired;

        private void Start()
        {
            if (currentDelay == -1)
                RandomizeDelay();

            StartCoroutine(FireDelayedEvent());
        }

        private IEnumerator FireDelayedEvent()
        {
            while (repeatCount > 0 || repeatCount == -1)
            {
                while (!enabled || !conditions.Value())
                    yield return new WaitForSeconds(1);

                if (currentDelay > 0)
                    yield return new WaitForSeconds(currentDelay);

                OnEventFired.Invoke();

                if (repeatCount != -1)
                    --repeatCount;

                RandomizeDelay();
                yield return new WaitForFixedUpdate();
            }
        }

        private void RandomizeDelay()
        {
            currentDelay = Random.Range(delayFrom, delayTo);
        }
    }
}
