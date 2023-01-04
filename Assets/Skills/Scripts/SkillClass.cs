using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClass : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifeTime;
    public void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
    }
    
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damage();
        }
    }
    
    public virtual void Move()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    public virtual void Damage()
    {
        
    }
}
