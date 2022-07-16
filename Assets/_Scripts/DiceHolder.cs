using System;
using UnityEngine;

namespace _Scripts
{
    public class DiceHolder : MonoBehaviour
    {
        public StatsDice dice;
        [SerializeField] private int _currentHealth;
        public int maxHealth;

        private void Start()
        {
            SetHealth();
        }

        public void SetHealth() => maxHealth = _currentHealth;

        public void TakeDamage(int damage) => _currentHealth -= damage;
    }
}