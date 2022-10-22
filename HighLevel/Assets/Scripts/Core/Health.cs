using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealthPoints = 100f;
        [SerializeField] private float healthPoints;
        private bool isDead = false;

        private void Start()
        {
            healthPoints = maxHealthPoints;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage) 
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            
            if (healthPoints == 0)
            {
                Die();
            }
        }

        

        public void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public float GetHealth()
        {
            return healthPoints;
        }

        public float GetMaxHealth()
        {
            return maxHealthPoints;
        }
    }
}
