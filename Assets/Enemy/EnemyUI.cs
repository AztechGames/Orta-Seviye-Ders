using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Image HealthBar;
    
    public float maxHealth;
    
    private float _health;
    public float EnemyHealth{
        get { return _health; }
        set
        {
            _health = value;
            HealthBar.fillAmount = _health / maxHealth;
            if(_health <= 0)
            {
                Die();
            }
        }
    }
    
    Camera cam;
    void Start()
    {
        EnemyHealth = maxHealth;
        cam = Camera.main;
    }
    
    void Update()
    {
        HealthBar.transform.parent.LookAt(cam.transform);
    }

    public void Die()
    {
        GetComponent<RewardDrop>().Drop();
        Destroy(gameObject);
    }
}
