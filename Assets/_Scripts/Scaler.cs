using UnityEngine;

namespace _Scripts
{
    [System.Serializable]
    public class Scaler
    {
        [SerializeField] private float m;
        [SerializeField] private float b;

        public float Value(int x) => m * x + b;
    }
}