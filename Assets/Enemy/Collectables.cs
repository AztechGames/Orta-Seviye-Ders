using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum DropType
    {
        Health,
        Energy,
        EXP,
    }
    public DropType dropType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.GetComponentInParent<PlayerUI>());
        }
    }

    void Collect(PlayerUI playerUI)
    {
        switch (dropType)
        {
            case DropType.Health: playerUI.Health += 10;
                break;
            case DropType.Energy: playerUI.Energy += 20;
                break;
            case DropType.EXP: playerUI.EXP += 20;
                break;
        }
        Destroy(gameObject);
    }
}
