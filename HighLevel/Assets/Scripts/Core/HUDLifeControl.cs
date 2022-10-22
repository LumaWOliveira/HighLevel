using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.Core
{
    public class HUDLifeControl : MonoBehaviour
    {

        [SerializeField] private Slider hP;
        [SerializeField] private Slider skullHP;
        [SerializeField] private Image face;


        [SerializeField] private Health health;

        private void Update()
        {
            hP.value = health.GetHealth() / health.GetMaxHealth();
        }



    }
}
