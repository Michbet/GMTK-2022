using System;
using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class Oscillator : MonoBehaviour
    {
        [SerializeField] private bool oscillate = true;
        [SerializeField] private bool useCurve;
        [SerializeField] private float amplitude = 1;
        [SerializeField] private float frequency = 1;
        [SerializeField] private AnimationCurve oscillationCurve;

        private Vector3 _startPos;
        private float Distance => Vector3.Distance(_startPos, GetPosition);
        private Vector3 GetPosition => transform.localPosition;
        private void SetPosition(Vector3 v) => transform.localPosition = v;

        private void Start()
        {
            _startPos = GetPosition;
            if(oscillate)
                StartCoroutine(OscillateRoutine());
        }

        public void Oscillate()
        {
            if(oscillate || Distance > .001f)
                return;
            oscillate = true;
            StartCoroutine(OscillateRoutine());
        }

        public void StopOscillating() => oscillate = false;

        private IEnumerator OscillateRoutine()
        {
            float startTime = Time.time;
            
            // main loop
            while (oscillate)
            {
                SetPosition(GetOscillatedPosition(Time.time - startTime));
                yield return null;
            }

            // trying to stop loop
            while (!oscillate && Distance > .001f)
            {
                SetPosition(GetOscillatedPosition(Time.time - startTime));
                yield return null;
            }

            transform.localPosition = _startPos;
        }
        
        private Vector3 GetOscillatedPosition(float timePassed) => useCurve ? 
            _startPos + oscillationCurve.Evaluate(timePassed) * Vector3.up : 
            _startPos + amplitude * Mathf.Sin(timePassed * frequency) * Vector3.up;


    }
}