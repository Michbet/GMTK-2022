using UnityEngine;

namespace _Scripts.Editor
{
    #if UNITY_EDITOR
    using UnityEditor;
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