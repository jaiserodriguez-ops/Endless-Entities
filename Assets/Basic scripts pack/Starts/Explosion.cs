using UnityEngine;

namespace Basic.AddForces
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _force = 700f;
        [SerializeField] private float _upwardsModifier = 3f;
        [SerializeField] private LayerMask _affectedLayers;

        public void Start()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _affectedLayers);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(_force, transform.position, _radius, _upwardsModifier, ForceMode.Impulse);
                }
            }
        }
    }
}
