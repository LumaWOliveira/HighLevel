using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWeapon : MonoBehaviour
{
    public GameObject enemy;

    private void Awake()
    {
        enemy = null;
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            enemy = target.gameObject;
        }

    }

    private void OnTriggerExit(Collider target)
    {
        enemy = null;
    }
}
