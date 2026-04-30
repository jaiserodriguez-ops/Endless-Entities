using UnityEngine;

namespace Basic.Renders
{
    public class SecurityCameraDisplay : MonoBehaviour
    {
        [SerializeField] private Camera _securityCamera;
        [SerializeField] private Material _displayMaterial;
        [Tooltip("Depth cant be < 1")][SerializeField] private Vector3Int _resolutionDepth = new Vector3Int(1920, 1080, 24);

        [Tooltip("Resets color to your (white by default)")]
        [SerializeField] private bool _isResetColorForProperVisibility;
        [SerializeField] private Color _color = Color.white;
        private RenderTexture _cameraOutput;

        private void Start()
        {
            // Create render texture with camera resolution
            _cameraOutput = new RenderTexture(_resolutionDepth.x, _resolutionDepth.y, _resolutionDepth.z);
            _securityCamera.targetTexture = _cameraOutput;
            _displayMaterial.mainTexture = _cameraOutput;

            if (_isResetColorForProperVisibility)
                _displayMaterial.color = _color;
        }

        private void OnDestroy()
        {
            // Cleanup
            if (_cameraOutput != null)
                _cameraOutput.Release();
        }
    }
}
