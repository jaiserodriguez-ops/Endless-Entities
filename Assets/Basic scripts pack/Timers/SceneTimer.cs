using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basic.Timers
{
    public class SceneTimer : MonoBehaviour
    {
        [Header("Is in Update()")]
        [Tooltip("No need if not keeping to next scene")][SerializeField] private bool _disableScriptAfter;
        [SerializeField] private float _timer = 13f;
        [SerializeField] private string _sceneName = "1";
        private float _interval;

        void Update()
        {
            _interval += Time.deltaTime;

            if (_interval >= _timer)
            {
                SceneManager.LoadScene(_sceneName);
                if (_disableScriptAfter)
                {
                    this.enabled = false;
                }
            }

        }

    }

}
