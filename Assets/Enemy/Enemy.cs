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
    public Transform _player;
    
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

    void Chase()
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
                Attack();
            }
            else _agent.isStopped = true;
        }
    }
    void Attack()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0)
        {
            _player.GetComponent<PlayerUI>().Health -= attackDamage;
            attackTimer = attackRate;
        }
    }
}
