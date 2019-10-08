using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private State state = State.PATROL;
   private int direction = 1;

   public Transform GroundDetection;
   public float viewAngle;

   public enum State{
      PATROL,
      PURSUE,
      ATTACK
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public override void HandleAI(Transform target){
      switch(state)
      {
         case State.PATROL:
            Patrol();
            break;
         case State.PURSUE:
            Pursue();
            break;
         case State.ATTACK:
            Attack();
            break;
      }
   }

   // ------------------------------------------------------
   // States
   // ------------------------------------------------------
   private void Patrol(){
      rb.velocity = new Vector2(direction*speed, 0);

      RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, 2f);
      RaycastHit2D wallInfo = Physics2D.Raycast(GroundDetection.position, GroundDetection.forward, 0.5f);

      if(!groundInfo.collider || !wallInfo.collider){
         direction *= -1;
         Vector3 newScale = transform.localScale;
         newScale.x *= -1;
         transform.localScale = newScale; 
      }

   }

   private void Pursue(){

   }

   private void Attack(){

   }
}
