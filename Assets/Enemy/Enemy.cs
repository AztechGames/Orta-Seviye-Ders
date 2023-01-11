using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float attackDistance;
    public float attackDamage;
    public float attackRate;

    private float _health;
    public float EnemyHealth{
        get { return _health; }
        set
        {
            _health = value;
            if(_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private float attackTimer;
    
    private NavMeshAgent _agent;
    private Transform _player;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        attackTimer = attackRate;
        _agent.speed = speed;
    }

    void Update()
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
