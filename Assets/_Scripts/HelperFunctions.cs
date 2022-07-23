using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts
{
    public static class HelperFunctions
    {
        private static PointerEventData _eventCurrentPosition;
        private static List<RaycastResult> _results;
        
        public static bool IsMouseOverButtonUI()
        {
            _eventCurrentPosition = new PointerEventData(EventSystem.current) {position = Input.mousePosition};
            _results = new List<RaycastResult>();
            
            EventSystem.current.RaycastAll(_eventCurrentPosition, _results);
            var count = _results.Count(result => result.gameObject.GetComponent<Button>() != null);
            return count > 0;
        }

        public static IEnumerator DoAfter(float duration, Action action)
        {
            yield return new WaitForSeconds(duration);
            action();
        }
    }
}