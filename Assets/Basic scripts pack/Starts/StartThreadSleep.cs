using System.Threading;
using UnityEngine;

namespace Basic.AwakeVoids
{
    public class StartThreadSleep : MonoBehaviour
    {
        [Header("Use this only when you know what you're doing.\nLike call before LoadSceneAsync or after it finished & freeze the game like its 2000's." +
            "\nDo not call at start of scene when testing, as it will just take longer to open the scene")]
        [Tooltip("1000 ms is 1s. Ranged to prevent you getting in trouble")][Range(0, 10000)] public int msToSleep = 1000;

        private void Start()
        {
            Thread.Sleep(msToSleep);
        }
    }
}