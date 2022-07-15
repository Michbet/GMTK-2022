using UnityEditor;
using UnityEngine;

namespace _Scripts
{
    #if UNITY_EDITOR
    [CustomEditor(typeof(EnemyDice))]
    public class EnemyDiceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("GenerateDice"))
            {
                var enemyDice =  target as EnemyDice;
                if(enemyDice)
                    enemyDice.GenerateDice();
            }
        }
    }
    #endif
}