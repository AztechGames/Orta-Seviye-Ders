using UnityEngine;

public class Shield : SkillClass
{
    public float shieldDuration = 5f;
    public override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            shieldDuration --;
            if(shieldDuration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
