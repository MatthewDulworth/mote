using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAI : Enemy
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool reflected;
   private GroundDetector groundDetector;
   private StateMachine<GroundEnemyAI> machine;

   [SerializeField] private int movementDirection = 1;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void OnStart(){
      groundDetector = GetComponent<GroundDetector>();
      machine = new GE_StateMachine(this);
      reflected = false;

      if(movementDirection == -1){
         flip();
      }
   }

   public override void OnFixedUpdate(){
      groundDetector.DetectGround();
      machine.OnStateFixedUpdate();
   }

   public override void OnUpdate(){
      HandleTargeting();
      machine.OnStateUpdate();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public bool OnGround(){
      return groundDetector.OnGround;
   }

   public void flip(){
      movementDirection *= -1;

      Vector3 newScale = transform.localScale;
      newScale.x *= -1;
      transform.localScale = newScale;

      reflected = !reflected;
      fov.ReflectOverXAxis(reflected);
   }
}
