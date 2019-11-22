using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargerEnemy
{
   public class Charge : State<AI>
   {
      // ------------------------------------------------------
      // Member Vars
      // ------------------------------------------------------
      private bool chargeDone;
      private Vector3 targetPosition;
      private Vector2 direction;

      // ------------------------------------------------------
      // Constructor
      // ------------------------------------------------------
      public Charge(AI owner, StateMachine machine)
      {
         this.owner = owner;
         this.machine = machine;
      }

      // ------------------------------------------------------
      // Updates
      // ------------------------------------------------------
      public override void OnUpdate()
      {
         List<string> stopChargeTags = new List<string>() { "Player", "Wall" };

         if (owner.HitBox.IsCollidingWith(stopChargeTags))
         {
            owner.StopMoving();
            chargeDone = true;
         }
         else
         {
            owner.ChangeVelocity(direction * owner.ChargeForce);
         }
      }

      public override void OnFixedUpdate()
      {

      }

      // ------------------------------------------------------
      // Enter/Exit
      // ------------------------------------------------------
      public override void OnEnter()
      {
         owner.StopMoving();
         chargeDone = false;

         targetPosition = owner.CurrentTarget.position;
         direction = targetPosition - owner.transform.position;
         direction.Normalize();
      }

      public override void OnExit()
      {
         owner.StartCooldown();
      }


      // ------------------------------------------------------
      // HandleStateChanges
      // ------------------------------------------------------
      public override void HandleStateChanges()
      {
         if (chargeDone)
         {
            machine.ChangeState(StateMachine.IDLE);
         }
      }
   }
}


