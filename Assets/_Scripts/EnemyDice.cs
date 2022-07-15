using System;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
    public class EnemyDice : MonoBehaviour
    {
        [Range(0, 1)]
        public float speedPercent = .39f;
        [Range(0, 1)]
        public float blockPercent = .09f;
        [Range(0, 1)]
        public float attackPercent = .52f;

        public int totalValue;

        public StatsDice dice;

        public void GenerateDice()
        {
            dice = StatsDice.GenerateStatsDice(totalValue, speedPercent, blockPercent, attackPercent);
            
        }
    }
}