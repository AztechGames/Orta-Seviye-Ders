using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 0;
    public float chaseDistance = 0;
    public float attackDistance = 0;
    public float attackDamage = 0;
    public float attackRate = 0;

    private float attackTimer;
    
    private NavMeshAgent _agent;
    
    [HideInInspector] public Transform _player;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        attackTimer = attackRate;
        _agent.speed = speed;
    }

    void Update()
    {
        Chase();
    }

    public virtual void Chase()
    {
        if (_player != null)
        {
            float distance = Vector3.Distance(_player.position, transform.position);
            if (distance < chaseDistance && distance >= attackDistance)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_player.position);
                transform.rotation = Quaternion.Lerp(Quaternion.Euler(_player.transform.position),transform.rotation,1f);
            }
            else if (distance < attackDistance)
            {
                _agent.stoppingDistance = attackDistance - 0.1f;
                AttackRate();
            }
            else _agent.isStopped = true;
        }
    }
    public virtual void AttackRate()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0)
        {
            Attack();
            attackTimer = attackRate;
        }
    }

    public virtual void Attack()
    {
        _player.GetComponent<PlayerUI>().Health -= attackDamage;
    }
    //int numberofobj , float radius , for (float angle = i * mathf.pı * 2 / numberofobj )
    // float x = mathf.cos(angle) * radius;
    // float z = mathf.sin(angle) * radius;
    // vector3 pos = transform pos + new vector3(x,0,z);
    // flaot angledegrees = angle * mathf.rad2deg;
    // quaternion rot = quaternion.euler(0,angledegrees,0);
    // gameobject obj = instantiate(prefab,pos,rot);
}
