using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : State<GroundEnemyAI>
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private float jumpAttackCoolDown;
   private float jumpAttackCoolDownLeft;

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
      HandleJumpAttackCoolDown();
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
      jumpAttackCoolDownLeft = jumpAttackCoolDown;
   }

   private void HandleJumpAttackCoolDown(){
      if(jumpAttackCoolDownLeft > 0.0f){
         jumpAttackCoolDownLeft -= Time.deltaTime;
      }
   }


   // ------------------------------------------------------
   // State Changes
   // ------------------------------------------------------
   public override void HandleStateChanges(){
      if(!owner.OnGround()){
         machine.ChangeState(GE_StateMachine.FALL);
      }
      else if(!owner.TargetSighted()){
         machine.ChangeState(GE_StateMachine.PATROL);
      }
   }

   public override void OnEnter(){
      owner.StopMoving();
      jumpAttackCoolDown = owner.JumpAttackCoolDown;
   }
}
