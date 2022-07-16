using System;
using UnityEngine;

namespace _Scripts
{
    public class DiceHolder : MonoBehaviour
    {
        public StatsDice dice;
        public Animator anim;
        public int currentHealth;
        public int maxHealth = 15;

        private void Start()
        {
            ResetHealth();
            anim = anim == null ? GetComponent<Animator>() : anim;
        }

        public void ResetHealth() => currentHealth = maxHealth;

        /// <summary>
        /// damages player
        /// </summary>
        /// <param name="damage"></param>
        /// <returns>true if the diceHolder died</returns>
        public bool TakeDamage(int damage)
        {
            currentHealth -= damage;
            return currentHealth <= 0;
        }

        public void PlayAnim(string stateName)
        {
            anim.Play(stateName);
        }
        
    }
}