using System;
using UnityEngine;

public class Halo : SkillClass
{
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyUI>().EnemyHealth -= damage;
        }
    }
}
