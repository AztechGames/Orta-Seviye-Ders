using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float attackThreshold1 = 0.5f;
    public float attackThreshold2 = 0.25f;

    public int numFireball = 8;
    
    public GameObject fireBall;
    public GameObject SmashEffect;

    private EnemyUI _enemyUI;
    private int skill;
    
    private enum AttackState
    {
        Attack,
        Fireball,
        Smash,
    }
    private AttackState currentAttackState;

    public override void Start()
    {
        base.Start();
        _enemyUI = GetComponent<EnemyUI>();
        skill = 1;
    }

    void AttackSelecter()
    {
        float healthPercentage = _enemyUI.EnemyHealth / _enemyUI.maxHealth;
        
        if (healthPercentage > attackThreshold2 && healthPercentage < attackThreshold1) skill = 2;
        
        else if (healthPercentage < attackThreshold2) skill = 3;

        else skill = 1;
        
        int randomAttack = Random.Range(0, skill);

        currentAttackState = (AttackState)randomAttack;
    }

    public override void Attack()
    {
      AttackSelecter();
      switch (currentAttackState)
      {
          case AttackState.Attack: SimpleAttack();
              break;
          case AttackState.Fireball: Fireball();
              break;
          case AttackState.Smash: Smash();
              break;
      }
    }

    void SimpleAttack()
    {
        _player.GetComponent<PlayerUI>().Health -= attackDamage;
    }

    void Fireball()
    {
        float angleStep = 360f / numFireball;
        for (int i = 0; i < numFireball; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * 4f;
            var fireobject= Instantiate(fireBall, spawnPosition + Vector3.up, Quaternion.identity);
            fireobject.transform.rotation = Quaternion.LookRotation(spawnPosition - transform.position);
        }
    }

    void Smash()
    {
       var smashObj = Instantiate(SmashEffect,transform.position, Quaternion.identity);
       Destroy(smashObj,3);
    }
}
