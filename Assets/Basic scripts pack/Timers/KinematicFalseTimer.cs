using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Basic.Timers
{
    public class KinematicFalseTimer : MonoBehaviour
    {
        [Header("(Is Async)\n\n2D")]
        [Tooltip("Sets RigidbodyType2D.Dynamic (instead of deprecated rb.isKinematic).")]
        [SerializeField] private Rigidbody2D[] _rigidbody2Ds;

        [Header("3D")]
        [Tooltip("Unity still uses isKinematic here.")][SerializeField] private Rigidbody[] _rigidbodies;



        [SerializeField] private float _timer = 3f;
        [SerializeField] private bool _DisableGameObjAfterUse = false;

        private async void Start()
        {
            await WaitAndKinematicFalseAsync();
        }

        private async Task WaitAndKinematicFalseAsync()
        {

            await Task.Delay(TimeSpan.FromSeconds(_timer));

            if (_rigidbodies != null)
                foreach (Rigidbody rb in _rigidbodies)
                {
                    if (rb != null)
                        rb.isKinematic = false;
                }

            if (_rigidbody2Ds != null)
                foreach (Rigidbody2D rb in _rigidbody2Ds)
                    if (rb != null)
                        rb.bodyType = RigidbodyType2D.Dynamic;

            if (_DisableGameObjAfterUse)
            {
                gameObject.SetActive(false);
            }

        }

        public void ManualTimerLaunch()
        {
            _ = WaitAndKinematicFalseAsync();
        }


    }
}
