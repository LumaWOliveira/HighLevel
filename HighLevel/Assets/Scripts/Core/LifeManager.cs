using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class LifeManager : MonoBehaviour
    {
        [SerializeField] private float recoveryHP;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && (other.GetComponent<Health>().GetHealth() < other.GetComponent<Health>().GetMaxHealth()))
            {
                other.GetComponent<Health>().Recovery(recoveryHP);
                Destroy(this.gameObject);
            }
        }

    }
}
