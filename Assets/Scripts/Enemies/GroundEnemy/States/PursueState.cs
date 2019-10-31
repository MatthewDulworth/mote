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

      // // handle flipping 
      // if(owner.FacingLeft && owner.TargetOnLeftOrRight() == 1){
      //    Debug.LogFormat("FlippingRight, FacingLeft: {0}, Target: {1}", owner.FacingLeft, owner.TargetOnLeftOrRight());
      
      //    // if the enemy is facing to the left, but the target is on the right
      //    owner.FlipHorizontal();
      // }
      // else if(!owner.FacingLeft && owner.TargetOnLeftOrRight() == -1){
      //    Debug.LogFormat("FlippingLeft, FacingLeft: {0}, Target: {1}", owner.FacingLeft, owner.TargetOnLeftOrRight());
      //    // if the enemy is facing to the right, but the target is on the left
      //    owner.FlipHorizontal();
      // }

      // handle moving
      if(owner.TargetSighted() && !owner.TargetInRange()){
         owner.ChangeVelocityScaled(1,0);
      }
      else if(owner.TargetInRange()){
         owner.StopMoving();
      }
   }

   private void HandleAttacks(){
      if(owner.TargetInRange() && jumpAttackCoolDownLeft <= 0){
         JumpAttack();
      }
   }

   private void JumpAttack(){
      jumpAttackCoolDownLeft = jumpAttackCoolDown;
      Debug.Log("yeet");
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
