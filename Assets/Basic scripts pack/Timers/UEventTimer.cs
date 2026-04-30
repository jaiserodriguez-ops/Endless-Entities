using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Basic.Timers
{
    public class UEventTimer : MonoBehaviour
    {
        [Header("Is sync Task call but still goes in Thread.Sleep() state")]
        [SerializeField] private float _delay = 1f;
        [SerializeField] private bool _triggerOnStart;
        [SerializeField] private bool _repeating;
        [SerializeField] private UnityEvent _onTimerComplete;

        private CancellationTokenSource _cts;

        private void Start()
        {
            if (_triggerOnStart)
            {
                StartTimer();
            }
        }

        public void StartTimer()
        {
            StopTimer(); // Cancel any existing timer
            _cts = new CancellationTokenSource();

            if (_repeating)
            {
                _ = RepeatingTimerAsync(_cts.Token).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError($"Timer failed: {task.Exception}");
                    }
                });
            }
            else
            {
                _ = SingleTimerAsync(_cts.Token).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError($"Timer failed: {task.Exception}");
                    }
                });
            }
        }

        public void StopTimer()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async Task SingleTimerAsync(CancellationToken token)
        {
            try
            {
                await Task.Delay(Mathf.RoundToInt(_delay * 1000), token);
                if (!token.IsCancellationRequested)
                {
                    _onTimerComplete?.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
                // Timer was cancelled, that's ok
            }
        }

        private async Task RepeatingTimerAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(Mathf.RoundToInt(_delay * 1000), token);
                    if (!token.IsCancellationRequested)
                    {
                        _onTimerComplete?.Invoke();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Timer was cancelled, that's ok
            }
        }

        private void OnDestroy()
        {
            StopTimer();
        }
    }
}