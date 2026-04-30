using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Basic.Timers
{
    public class UEventTimer_Coroutine : MonoBehaviour
    {
        [Header("Coroutine based")]

        [SerializeField] private float _delay = 1f;
        [SerializeField] private bool _triggerOnStart;
        [SerializeField] private bool _repeating;
        [SerializeField] private UnityEvent _onTimerComplete;

        private void Start()
        {
            if (_triggerOnStart)
            {
                StartTimer();
            }
        }

        public void StartTimer()
        {
            if (_repeating)
            {
                StartCoroutine(RepeatingTimer());
            }
            else
            {
                StartCoroutine(SingleTimer());
            }
        }

        public void StopTimer()
        {
            StopAllCoroutines();
        }

        private IEnumerator SingleTimer()
        {
            yield return new WaitForSeconds(_delay);
            _onTimerComplete?.Invoke();
        }

        private IEnumerator RepeatingTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);
                _onTimerComplete?.Invoke();
            }
        }
    }
}