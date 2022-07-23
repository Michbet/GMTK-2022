using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts
{
    public class Oscillator : MonoBehaviour
    {
        [SerializeField] private bool oscillate = true;
        [SerializeField] private bool useCurve;
        [SerializeField] private float amplitude = 1;
        [SerializeField] private float frequency = 1;
        [SerializeField] private AnimationCurve oscillationCurve = AnimationCurve.Constant(0, 1, 0);
        [Range(0, 2 * Mathf.PI)]
        [SerializeField] private float phaseDifference;

        private Vector3 _startPos;
        private float Distance => Vector3.Distance(_startPos, GetPosition);

        private RectTransform _rectTransform;
        private Vector3 GetPosition => _rectTransform == null ? transform.localPosition : (Vector3)_rectTransform.anchoredPosition;

        private void SetPosition(Vector3 v)
        {
            if (_rectTransform == null)
                transform.localPosition = v;
            else
                _rectTransform.anchoredPosition = v;
        }

        private void OnEnable()
        {
            _rectTransform = transform as RectTransform;
            _startPos = GetPosition;
            if (oscillate)
            {
                StopCoroutine(OscillateRoutine());
                StartCoroutine(OscillateRoutine());
            }
        }

        private void OnDisable()
        {
            // StopOscillating();
            // SetPosition(_startPos);
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
            _startPos + oscillationCurve.Evaluate(timePassed + phaseDifference) * Vector3.up : 
            _startPos + amplitude * Mathf.Sin(timePassed * frequency + phaseDifference) * Vector3.up;


    }
}