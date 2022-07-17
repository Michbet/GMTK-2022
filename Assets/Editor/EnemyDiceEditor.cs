//
// #if UNITY_EDITOR
// using UnityEngine;
// using UnityEditor;
// using _Scripts;
//
// [CustomEditor(typeof(EnemyDice))]
// public class EnemyDiceEditor : UnityEditor.Editor
// {
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//         if (GUILayout.Button("GenerateDice"))
//         {
//             var enemyDice =  target as EnemyDice;
//             if(enemyDice)
//                 enemyDice.GenerateDice();
//         }
//     }
// }
// #endif