using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : State<GroundEnemyAI>
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private float attackCoolDownTime;
   private float coolDownLeft;

   // ------------------------------------------------------
   // Constructor
   // ------------------------------------------------------
   public PursueState(GroundEnemyAI owner, GE_StateMachine machine){
      this.machine = machine;
      this.owner = owner;
   }


   // ------------------------------------------------------
   // Updates
   // ------------------------------------------------------
   public override void OnUpdate(){
      HandleCooldown();
   }
   public override void OnFixedUpdate(){
      HandleMovement();
      HandleAttacks();
   }


   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void HandleMovement(){
      if(owner.TargetSighted()){
         owner.MoveToTargetX();
      }
   }

   private void HandleAttacks(){

   }

   private void JumpAttack(){

   }

   private void HandleCooldown(){
      if(coolDownLeft > 0.0f){
         coolDownLeft -= Time.deltaTime;
      }
   }

   public void StartCoolDown(){
      coolDownLeft = attackCoolDownTime;
   }


   // ------------------------------------------------------
   // State Changes
   // ------------------------------------------------------
   public override void HandleStateChanges(){
      
   }

   public override void OnEnter(){
      owner.StopMoving();
      attackCoolDownTime = owner.AttackCooldownTime;
   }
}
