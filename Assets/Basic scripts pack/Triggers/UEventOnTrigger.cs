using UnityEngine;
using UnityEngine.Events;

namespace Basic.Triggers
{
    public class UEventOnTrigger : MonoBehaviour
    {
        [Header("(Handles both 2D and 3D)\n\n(Important tips:\nDisable after enter to prevent repeating. \nExclude all layers except player in collider)" +
            "\n\nAfter enter")]
        [Tooltip("This blocks OnTriggerExit")][SerializeField] private bool _DisableScriptAfterEnter;
        [Tooltip("This blocks OnTriggerExit")][SerializeField] private bool _DisableGameObjectAfterEnter;

        public UnityEvent _OnTriggerEnter;
        public UnityEvent _OnTriggerExit;
        public UnityEvent _OnTriggerStay;

        private void OnTriggerEnter(Collider other)
        {
            TriggerEntersLogic();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerEntersLogic();
        }

        private void OnTriggerExit(Collider other)
        {
            _OnTriggerExit?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _OnTriggerExit?.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            _OnTriggerStay?.Invoke();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            _OnTriggerStay?.Invoke();
        }

        private void TriggerEntersLogic()
        {
            _OnTriggerEnter?.Invoke();
            CheckDisables();
        }

        private void CheckDisables()
        {
            if (_DisableScriptAfterEnter)
                this.enabled = false;
            if (_DisableGameObjectAfterEnter)
                gameObject.SetActive(false);
        }


    }
}
