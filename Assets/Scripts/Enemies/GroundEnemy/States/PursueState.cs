﻿using System.Collections;
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
   public PursueState(GroundEnemyAI owner, GE_StateMachine machine)
   {
      this.machine = machine;
      this.owner = owner;
   }


   // ------------------------------------------------------
   // Updates
   // ------------------------------------------------------
   public override void OnUpdate()
   {
      HandleJumpAttackCoolDown();
   }
   public override void OnFixedUpdate()
   {
      HandleMovement();
      HandleAttacks();
   }


   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void HandleMovement()
   {

      // if the enemy is facing to the left, but the target is on the right, or vice versa
      if ((owner.FacingLeft && owner.TargetOnLeftOrRight() == 1) || (!owner.FacingLeft && owner.TargetOnLeftOrRight() == -1))
      {
         owner.FlipHorizontal();
      }

      // handle moving
      if (owner.TargetSighted() && !owner.TargetInRange())
      {
         owner.ChangeVelocityScaled(1, 0);
      }
      else if (owner.TargetInRange() || UnderneathTarget())
      {
         owner.StopMoving();
      }
   }

   private void HandleAttacks()
   {
      if (owner.TargetInRange() && jumpAttackCoolDownLeft <= 0)
      {
         JumpAttack();
      }
      else if (UnderneathTarget() && jumpAttackCoolDownLeft <= 0)
      {
         JumpUpAttack();
      }
   }

   private void JumpAttack()
   {
      jumpAttackCoolDownLeft = jumpAttackCoolDown;
      owner.ChangeVelocity(owner.JumpSpeed * owner.MovementDirection / 2.0f, owner.JumpSpeed);
   }

   private void JumpUpAttack()
   {
      jumpAttackCoolDownLeft = jumpAttackCoolDown;
      owner.ChangeVelocity(0, owner.JumpSpeed);
   }

   private void HandleJumpAttackCoolDown()
   {
      if (jumpAttackCoolDownLeft > 0.0f)
      {
         jumpAttackCoolDownLeft -= Time.deltaTime;
      }
   }

   private bool UnderneathTarget()
   {
      return Mathf.Abs(owner.transform.position.x - owner.CurrentTarget.transform.position.x) <= 0.5;
   }

   // ------------------------------------------------------
   // State Changes
   // ------------------------------------------------------
   public override void HandleStateChanges()
   {
      if (!owner.OnGround())
      {
         machine.ChangeState(GE_StateMachine.FALL);
      }
      else if (!owner.TargetSighted())
      {
         machine.ChangeState(GE_StateMachine.PATROL);
      }
   }

   public override void OnEnter()
   {
      owner.StopMoving();
      jumpAttackCoolDown = owner.JumpAttackCoolDown;
   }
}
