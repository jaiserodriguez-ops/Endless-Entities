using UnityEngine;

namespace Basic.Sounds
{
    public class ChildCollisionReporter2D : MonoBehaviour
    {
        [Header("Dependency Check")]
        [Tooltip("Will only report collisions when this component is not destroyed")]
        [SerializeField] private Component _requiredComponent;
        [SerializeField] private bool _useRequiredComponent;

        private CollisionSoundHandler2D _parentSoundHandler;

        private void Start()
        {
            // Find the parent sound handler
            _parentSoundHandler = GetComponentInParent<CollisionSoundHandler2D>();

            if (_parentSoundHandler == null)
            {
                Debug.LogWarning($"ChildCollisionReporter2D on {gameObject.name} could not find CollisionSoundHandler2D in parent hierarchy. Disabling component.");
                this.enabled = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_useRequiredComponent)
                // Check if required component is missing or destroyed
                if (_requiredComponent == null)
                    return;

            if (_parentSoundHandler != null)
            {
                float collisionForce = collision.relativeVelocity.magnitude;
                Vector2 collisionPoint = collision.contacts.Length > 0 ? collision.contacts[0].point : (Vector2)transform.position;
                _parentSoundHandler.HandleChildCollision(collisionForce, collision.gameObject, collisionPoint);
            }
        }
    }
}