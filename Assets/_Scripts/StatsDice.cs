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
            
            Debug.Log("Total: " + totalValue);
            Debug.Log("Speed Total: " + stats.speedDie.faces.Sum());
            Debug.Log("Block Total: " + stats.blockDie.faces.Sum());
            Debug.Log("Attack Total: " + stats.attackDie.faces.Sum());
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
                dice[i] = Die.GenerateDie(Mathf.RoundToInt(totalValue * dicePercent[i]));
                // sort the faces
                var sortedFaces = dice[i].faces.ToList();
                sortedFaces.Sort();
                dice[i].faces = sortedFaces.ToArray();
            }

            var difference = totalValue - dice.Sum(die => die.Total);
            for (int i = 0; i < difference; i++)
            {
                dice[Random.Range(0, dice.Length)].AddRandom();
            }
            
            return dice;
        }
        
        // public static Die[] GenerateDice(int totalValue, params float[] diceWeights)
        // {
        //     Die[] dice = new Die[diceWeights.Length];
        //     for(int i = 0; i < dice.Length; i++)
        //     {
        //         dice[i] = new Die();
        //     }
        //
        //     var valuesRemaining = totalValue;
        //     while (valuesRemaining > 0)
        //     {
        //         dice[GetIndexFromWeights(diceWeights)].AddRandom();
        //         valuesRemaining--;
        //     }
        //     
        //     return dice;
        //
        //     int GetIndexFromWeights(float[] weights)
        //     {
        //         var totalWeight = diceWeights.Sum();
        //         // var sum = totalWeight;
        //         var value = Random.Range(0f, totalWeight);
        //         int i = 0;
        //         foreach (var weight in weights)
        //         {
        //             if (value < weight)
        //                 return i;
        //             value -= weight;
        //             i++;
        //         }
        //
        //         return -1;
        //     }
        // }
        

        public TurnStats Roll()
        {
            return new TurnStats(speedDie.Roll(), blockDie.Roll(), attackDie.Roll());
        }

        public int TotalValue => speedDie.faces.Sum() + blockDie.faces.Sum() + attackDie.faces.Sum();
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
        
        public static TurnStats NotDecided = new TurnStats(-1, -1, -1);
    }
}