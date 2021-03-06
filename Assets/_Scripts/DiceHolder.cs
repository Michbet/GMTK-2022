using System;
using UnityEngine;

namespace _Scripts
{
    public class DiceHolder : MonoBehaviour
    {
        public StatsDice dice;
        public Animator anim;
        public float currentHealth;
        public float maxHealth = 15;

        public OverlayAnimation overlayAnimation;

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
        public bool TakeDamage(float damage)
        {
            currentHealth -= damage;
            return currentHealth <= 0;
        }

        public void PlayAnim(string stateName)
        {
            anim.Play(stateName);
        }

        public void PlayOverlayAnim(string stateName)
        {
            overlayAnimation.Play(stateName);
        }
    }
}