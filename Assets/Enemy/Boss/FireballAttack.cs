using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : SkillClass
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerUI>().Health -= damage;
        }
    }
}
