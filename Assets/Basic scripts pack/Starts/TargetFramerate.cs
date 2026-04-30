using UnityEngine;

namespace Basic.StartVoids
{
    public class TargetFramerate : MonoBehaviour
    {
        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        }

    }

}
