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
            other.GetComponent<EnemyUI>().EnemyHealth -= damage;
        }
    }
    
    public virtual void Move()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }
    
}
