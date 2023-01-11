using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image HealthBar, EnergyBar;

    public float EnergyFillAmount;
    
    [HideInInspector] public bool vulnerable = true;
    
    private float _health;
    private float _energy;

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (vulnerable)
            {
                _health = value;
                _health = Mathf.Clamp(_health, 0, 100);
                HealthBar.fillAmount = _health / 100;
            }
        }
    }
    public float Energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
            _energy = Mathf.Clamp(_energy, 0, 100);
            EnergyBar.fillAmount = _energy / 100;
        }
    }

    private void Start()
    {
        Health = 100;
        Energy = 100;
    }

    void Update()
    {
        if(Energy < 100) Energy += EnergyFillAmount * Time.deltaTime;
    }
}
