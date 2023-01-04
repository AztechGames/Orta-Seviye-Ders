using UnityEngine;

public class Fireball : SkillClass
{
   public override void OnTriggerEnter(Collider other)
   {
      base.OnTriggerEnter(other);
      if(!other.CompareTag("Player"))
         Destroy(gameObject);
   }
}
