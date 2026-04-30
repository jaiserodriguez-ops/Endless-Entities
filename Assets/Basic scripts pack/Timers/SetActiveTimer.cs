using UnityEngine;

namespace Basic.Timers
{
    public class SetActiveTimer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _targetObjects;
        [SerializeField] private float _timer = 3f;
        [Header("(Priority is same)")]
        [SerializeField] private bool _deactivateGameObjectAfterUse; // Do not change to true and rename to not bug other script on same object
        [SerializeField] private bool _disableScriptAfterUse;
        [Header("Add source clip time to timer value? Non-array field can be empty")]
        [SerializeField] private bool _isUsingAudioTime;
        [SerializeField] AudioSource _audioSource;
        float _requiredSingleClipLength;

        [SerializeField] AudioSource[] _additionalSources;

        private float _interval;

        private void Update()
        {

            _interval += Time.deltaTime;
            if (!_isUsingAudioTime)
            {
                if (_interval >= _timer)
                {
                    foreach (GameObject obj in _targetObjects)
                    {
                        obj.SetActive(true);
                    }

                    Debug.DrawRay(transform.position, Vector3.zero, Color.green);
                    CheckDisablesAfterUse();

                }

            }
            else
            {
                if (_audioSource != null && _requiredSingleClipLength == 0)
                    _requiredSingleClipLength = _audioSource.clip.length;

                float totalExtraTime = 0;

                if (_additionalSources.Length > 0)
                {
                    totalExtraTime += _requiredSingleClipLength;
                    foreach (AudioSource source in _additionalSources)
                    {
                        totalExtraTime += source.clip.length;
                    }
                }
                else
                {
                    totalExtraTime = _requiredSingleClipLength;
                }

                if (_interval >= _timer + totalExtraTime)
                {
                    foreach (GameObject obj in _targetObjects)
                    {
                        obj.SetActive(true);
                    }

                    Debug.DrawRay(transform.position, Vector3.zero, Color.green);
                    CheckDisablesAfterUse();

                }

            }

        }

        void CheckDisablesAfterUse()
        {
            if (_deactivateGameObjectAfterUse)
            {
                gameObject.SetActive(false);
            }
            if (_disableScriptAfterUse)
            {
                this.enabled = false;
            }
        }

    } // class
}