using UnityEngine;

namespace Basic.Lighting
{
    public class LightFlicker : MonoBehaviour
    {
        [Header("Single light still must be assigned. Auto-gets if none")]
        [SerializeField] private Light _light;
        [SerializeField] private Light[] _lights;
        [SerializeField] private float _baseIntensity = 1f;
        [SerializeField] private float _flickerIntensity = 0.3f;
        [SerializeField] private float _flickerSpeed = 0.1f;
        [Header("Very slow and smooth")][SerializeField] private bool _smoothTransition;

        private float _randomOffset;
        private float _timer;

        private void Awake()
        {
            if (_light == null)
                _light = GetComponent<Light>();

            _randomOffset = Random.value * 100f; // Different starting point for multiple lights
        }

        private void Update()
        {
            if (_smoothTransition)
            {
                SmoothFlicker();
            }
            else
            {
                RandomFlicker();
            }
        }

        private void RandomFlicker()
        {
            _timer += Time.deltaTime;

            if (_timer >= _flickerSpeed)
            {
                _timer = 0f;
                _light.intensity = Random.Range(
                    _baseIntensity - _flickerIntensity,
                    _baseIntensity + _flickerIntensity
                );

                if (_lights != null)
                {
                    foreach (Light light in _lights)
                    {
                        light.intensity = Random.Range(
                        _baseIntensity - _flickerIntensity,
                        _baseIntensity + _flickerIntensity
                        );
                    }
                }

            }


        }

        private void SmoothFlicker()
        {
            float noise = Mathf.PerlinNoise(_randomOffset + Time.time * _flickerSpeed, 0f);
            _light.intensity = _baseIntensity + (noise - 0.5f) * _flickerIntensity * 2f;

            if (_lights != null)
            {
                foreach (Light light in _lights)
                { light.intensity = _baseIntensity + (noise - 0.5f) * _flickerIntensity * 2f; }
            }
        }
    }
}