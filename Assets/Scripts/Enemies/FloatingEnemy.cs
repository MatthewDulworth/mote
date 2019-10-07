using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : Enemy
{
   private bool hasTarget = false;

   public override void HandleMovement(Transform target){
      float distance = Vector2.Distance(transform.position, target.position);

      if(distance < sightRange){
         hasTarget = true;
      }

      if(distance > offsetFromPlayer && hasTarget){
         transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
      }
   }
}
