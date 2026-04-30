using System.Collections.Generic;
using UnityEngine;

namespace Basic.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class CollisionSoundHandler2D : MonoBehaviour
    {
        [Header("Audio Settings")]
        [Tooltip("Played in 2D, set spatial in AudioSource if needed")][SerializeField] private AudioClip[] _impactSounds;
        [Range(0, 1)][SerializeField] private float _baseVolume = 1f;
        [Tooltip("Doesn't work as you think at 222kg, .1 ~= .5.\nNear base pitch at 1kg")]
        [SerializeField] private float _basePitch = 1f;
        [SerializeField] private float _pitchRandomization = 0.05f;

        [Header("Velocity Settings")]
        [SerializeField] private float _minVelocity = 0.1f;
        [SerializeField] private float _maxVelocity = 10f;
        [SerializeField] private float _velocityToPitchMultiplier = 0.1f;
        [SerializeField] private float _velocityToVolumeMultiplier = 0.1f;

        [Tooltip("If detecting child collisions, only trigger on these tags (empty = all tags)")]
        [SerializeField] private string[] _allowedChildTags = new string[0];

        [Header("Cooldown")]
        [SerializeField] private float _minTimeBetweenSounds = 0.1f;

        [Header("Anti-Jiggle")]
        [SerializeField] private float _minCollisionPointDistance = 0.1f;
        [SerializeField] private int _maxStoredPoints = 5;

        private AudioSource _audioSource;
        private float _lastPlayTime;
        private Queue<Vector2> _previousCollisionPoints = new Queue<Vector2>();

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision.relativeVelocity.magnitude, collision.gameObject, collision.contacts[0].point);
        }

        // Method to handle collisions from child objects
        public void HandleChildCollision(float collisionForce, GameObject collisionObject = null, Vector2 collisionPoint = default)
        {
            HandleCollision(collisionForce, collisionObject, collisionPoint);
        }

        private void HandleCollision(float collisionForce, GameObject collisionObject = null, Vector2 collisionPoint = default)
        {
            if (Time.time - _lastPlayTime < _minTimeBetweenSounds) return;
            if (collisionForce < _minVelocity) return;

            // Check if this is a child collision and if we should process it
            if (collisionObject != null && collisionObject.transform != this.transform)
            {
                // Check tags if specified
                if (_allowedChildTags.Length > 0)
                {
                    bool tagAllowed = false;
                    foreach (string allowedTag in _allowedChildTags)
                    {
                        if (collisionObject.CompareTag(allowedTag))
                        {
                            tagAllowed = true;
                            break;
                        }
                    }
                    if (!tagAllowed) return;
                }
            }

            // Check collision point distance to prevent jiggling in place
            if (_minCollisionPointDistance > 0 && !IsCollisionPointFarEnough(collisionPoint))
                return;

            // Store the current collision point
            if (_minCollisionPointDistance > 0)
            {
                _previousCollisionPoints.Enqueue(collisionPoint);
                if (_previousCollisionPoints.Count > _maxStoredPoints)
                    _previousCollisionPoints.Dequeue();
            }

            // Calculate pitch
            float velocityPitchModifier = Mathf.Clamp(collisionForce * _velocityToPitchMultiplier, 0.1f, 2f);
            float randomPitchModifier = Random.Range(-_pitchRandomization, _pitchRandomization);
            float finalPitch = _basePitch + velocityPitchModifier + randomPitchModifier;

            // Calculate volume
            float velocityVolumeModifier = Mathf.Clamp(collisionForce / _maxVelocity, 0.1f, 1f);
            float finalVolume = _baseVolume + (velocityVolumeModifier * _velocityToVolumeMultiplier);

            // Select random sound
            if (_impactSounds.Length == 0) return;
            AudioClip randomSound = _impactSounds[Random.Range(0, _impactSounds.Length)];

            // Apply settings and play
            _audioSource.pitch = finalPitch;
            _audioSource.volume = finalVolume;
            _audioSource.clip = randomSound;
            if (_audioSource.enabled) // check if destroy this gO
                _audioSource.Play();

            _lastPlayTime = Time.time;
        }

        private bool IsCollisionPointFarEnough(Vector2 newPoint)
        {
            if (_previousCollisionPoints.Count == 0)
                return true;

            foreach (Vector2 previousPoint in _previousCollisionPoints)
            {
                if (Vector2.Distance(newPoint, previousPoint) < _minCollisionPointDistance)
                    return false;
            }

            return true;
        }
    }
}