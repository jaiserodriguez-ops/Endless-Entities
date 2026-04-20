using UnityEngine;

namespace Basic.PublicVariables
{
    /// <summary>
    /// e.g. use in use Unity Events + custom logic.
    /// </summary>
    public class PublicFloat : MonoBehaviour
    {
        [SerializeField] private float _float;

        public float FloatGetSet { get => _float; set => _float = value; }

        public void PrintFloat()
        {
            print(_float);
        }

    }

}