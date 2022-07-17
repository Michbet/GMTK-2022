// #if UNITY_EDITOR
// using UnityEditor;
// using _Scripts;
//
// [CustomEditor(typeof(DiceHolder))]
// public class DiceHolderEditor : UnityEditor.Editor
// {
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//         var diceHolder = target as DiceHolder;
//         if(diceHolder)
//             EditorGUILayout.SelectableLabel("Total value: " + diceHolder.dice.TotalValue.ToString());
//     }
// }
// #endif