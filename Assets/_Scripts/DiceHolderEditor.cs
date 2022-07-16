using UnityEditor;
using UnityEngine;

namespace _Scripts
{
    [CustomEditor(typeof(DiceHolder))]
    public class DiceHolderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var diceHolder = target as DiceHolder;
            if(diceHolder)
                EditorGUILayout.SelectableLabel("Total value: " + diceHolder.dice.TotalValue.ToString());
        }
    }
}