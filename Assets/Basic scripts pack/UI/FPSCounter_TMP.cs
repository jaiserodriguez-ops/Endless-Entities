using TMPro;
using UnityEngine;

namespace Basic
{
    public class FPSCounter_TMP : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;  // For UI display
        [Tooltip("Script is in Update() loop")][SerializeField] private float _updateInterval = 0.5f;  // How often to update the count

        private float _accum;
        private int _frames;
        private float _timeLeft;
        private float _fps;

        private void Update()
        {
            _timeLeft -= Time.deltaTime;
            _accum += Time.timeScale / Time.deltaTime;
            _frames++;

            if (_timeLeft <= 0f)
            {
                _fps = _accum / _frames;
                _timeLeft = _updateInterval;
                _accum = 0;
                _frames = 0;

                _fpsText.text = $"FPS: {_fps:N1}";  // Shows one decimal place
            }
        }
    }
}