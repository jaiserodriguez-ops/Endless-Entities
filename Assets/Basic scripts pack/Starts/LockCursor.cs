using UnityEngine;

namespace Basic.StartVoids
{
    public class LockCursor : MonoBehaviour
    {
        [Tooltip("CursorLockMode.None")][SerializeField] private bool _isInverted = false;

        void Start()
        {
            if (!_isInverted)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }
    }
}