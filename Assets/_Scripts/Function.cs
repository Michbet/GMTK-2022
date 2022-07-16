using UnityEditor;
using UnityEngine;

namespace _Scripts
{

    [System.Serializable]
    public abstract class Function
    {
        public abstract float Calculate(float x);
    }

    [System.Serializable]
    public class Linear : Function
    {
        public float m;
        [Tooltip("y intercept")] public float b;
        public override float Calculate(float x) => m * x + b;
    }
    
    [System.Serializable]
    public class Binomial : Function
    {
        public float a;
        public float b;
        [Tooltip("y intercept")] public float c;
        public override float Calculate(float x) => a * x * x + b * x + c;
    }
    
    [System.Serializable]
    public class Logarithm : Function
    {
        [Tooltip("multiplier")] public float m;
        [Tooltip("base")] public float b;
        [Tooltip("y intercept")] public float c;
        public override float Calculate(float x) => m * Mathf.Log(x, b) + c;
    }

    [System.Serializable]
    public class Exponential : Function
    {
        [Tooltip("base")]public float a;
        [Tooltip("y intercept")] public float b;

        public override float Calculate(float x) => Mathf.Pow(a, x) + b;
    }
}