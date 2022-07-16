using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    [System.Serializable]
    public struct StatsDice
    {
        public Die speedDie;
        public Die blockDie;
        public Die attackDie;

        public static StatsDice GenerateStatsDice(int totalValue, float speedPercent, float blockPercent, float attackPercent)
        {
            StatsDice stats = new StatsDice();
            Die[] generatedDice = GenerateDice(totalValue, speedPercent, blockPercent, attackPercent);
            stats.speedDie = generatedDice[0];
            stats.blockDie = generatedDice[1];
            stats.attackDie = generatedDice[2];
            return stats;
        }

        public static Die[] GenerateDice(int totalValue, params float[] dicePercent)
        {
            Die[] dice = new Die[dicePercent.Length];
            if (Mathf.RoundToInt(dicePercent.Sum()) != 1)
            {
                throw new Exception("Percents don't round to 1");
            }
            for (int i = 0; i < dicePercent.Length; i++)
            {
                dice[i] = Die.GenerateDie((int)(totalValue * dicePercent[i]));
                // sort the faces
                var sortedFaces = dice[i].faces.ToList();
                sortedFaces.Sort();
                dice[i].faces = sortedFaces.ToArray();
            }
            return dice;
        }

        public TurnStats Roll()
        {
            return new TurnStats(speedDie.Roll(), blockDie.Roll(), attackDie.Roll());
        }
    }

    public struct TurnStats
    {
        public int speed;
        public int block;
        public int attack;

        public TurnStats(int speed, int block, int attack)
        {
            this.speed = speed;
            this.block = block;
            this.attack = attack;
        }
    }
}