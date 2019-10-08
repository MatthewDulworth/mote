using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : Enemy
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool hasTarget = false;

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public override void HandleAI(Transform target){
      float distance = Vector2.Distance(transform.position, target.position);

      if(distance < viewRadius){
         hasTarget = true;
      }

      if(distance > offsetFromPlayer && hasTarget){
         transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
      }
   }
}
