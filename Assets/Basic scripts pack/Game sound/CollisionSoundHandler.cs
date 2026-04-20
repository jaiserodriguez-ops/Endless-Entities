using UnityEngine;
using Random = UnityEngine.Random;

namespace Basic.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class CollisionSoundHandler : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private AudioClip[] _impactSounds;
        [SerializeField] private float _baseVolume = 1f;
        [Header("Doesn't work as you think 222kg, .1 ~= .5")]
        [SerializeField] private float _basePitch = 1f;
        [SerializeField] private float _pitchRandomization = 0.05f;

        [Header("Velocity Settings")]
        [SerializeField] private float _minVelocity = 0.1f;
        [SerializeField] private float _maxVelocity = 10f;
        [SerializeField] private float _velocityToPitchMultiplier = 0.1f;
        [SerializeField] private float _velocityToVolumeMultiplier = 0.1f;

        [Header("Cooldown")]
        [SerializeField] private float _minTimeBetweenSounds = 0.1f;

        private AudioSource _audioSource;
        private float _lastPlayTime;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.spatialBlend = 1f; // Full 3D sound
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (Time.time - _lastPlayTime < _minTimeBetweenSounds) return;

            float collisionForce = collision.relativeVelocity.magnitude;
            if (collisionForce < _minVelocity) return;

            // Calculate pitch
            float velocityPitchModifier = Mathf.Clamp(collisionForce * _velocityToPitchMultiplier, 0.1f, 2f);
            float randomPitchModifier = Random.Range(-_pitchRandomization, _pitchRandomization);
            float finalPitch = _basePitch + velocityPitchModifier + randomPitchModifier;

            // Calculate volume
            float velocityVolumeModifier = Mathf.Clamp(collisionForce / _maxVelocity, 0.1f, 1f);
            float finalVolume = _baseVolume + (velocityVolumeModifier * _velocityToVolumeMultiplier);

            // Select random sound
            AudioClip randomSound = _impactSounds[Random.Range(0, _impactSounds.Length)];

            // Apply settings and play
            _audioSource.pitch = finalPitch;
            _audioSource.volume = finalVolume;
            _audioSource.clip = randomSound;
            if (_audioSource.enabled) // check if destroy this gO
                _audioSource.Play();

            _lastPlayTime = Time.time;
        }
    }
}
