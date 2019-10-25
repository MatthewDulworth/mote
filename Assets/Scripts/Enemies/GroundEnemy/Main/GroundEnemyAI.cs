using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAI : Enemy
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool reflected;
   private Transform currentTarget;
   private StateMachine<GroundEnemyAI> machine;
   private LayerMask wallLayer;

   [SerializeField] private int movementDirection = 1;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void OnStart(){
      machine = new GE_StateMachine(this);
      reflected = false;

      if(movementDirection == -1){
         flip();
      }
   }

   public override void OnFixedUpdate(){
      machine.OnStateFixedUpdate();
   }

   public override void OnUpdate(){
      machine.OnStateUpdate();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public bool TargetSighted(){
      return (currentTarget != null);
   }

   private bool TargetInRange(){
      return false;
   }

   private bool OnGround(){
      return false;
   }

   private void flip(){
      movementDirection *= -1;

      Vector3 newScale = transform.localScale;
      newScale.x *= -1;
      transform.localScale = newScale;

      reflected = !reflected;
      fov.ReflectOverXAxis(reflected);
   }
}
