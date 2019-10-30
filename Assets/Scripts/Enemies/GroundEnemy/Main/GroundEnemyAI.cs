using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAI : Enemy
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool reflected;
   private float coolDownLeft = 0;
   private GroundDetector groundDetector;
   private StateMachine<GroundEnemyAI> machine;

   [SerializeField] private int movementDirection = 1;
   [SerializeField] private float attackCooldownTime = 1.0f;

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

   public void stopMoving(){
      rb.velocity = Vector3.zero;
   }

   public void HandleCooldown(){
      if(AttackOnCooldown()){
         coolDownLeft -= Time.deltaTime;
      }
   }

   public void StartCoolDown(){
      coolDownLeft = attackCooldownTime;
   }

   public bool AttackOnCooldown(){
      return (coolDownLeft > 0);
   }
}

