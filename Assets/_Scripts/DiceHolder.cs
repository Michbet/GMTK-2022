using System;
using UnityEngine;

namespace _Scripts
{
    public class DiceHolder : MonoBehaviour
    {
        public StatsDice dice;
        private int _currentHealth;
        public int maxHealth = 15;

        private void Start()
        {
            ResetHealth();
        }

        public void ResetHealth() => _currentHealth = maxHealth;

        /// <summary>
        /// damages player
        /// </summary>
        /// <param name="damage"></param>
        /// <returns>true if the diceHolder died</returns>
        public bool TakeDamage(int damage)
        {
            _currentHealth -= damage;
            return _currentHealth <= 0;
        }
        
    }
}