using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargerEnemy
{
   public class Idle : State<AI>
   {
      // ------------------------------------------------------
      // Member Vars
      // ------------------------------------------------------
      private float idleTime = 1;
      private float idleTimeLeft;

      // ------------------------------------------------------
      // Constructor
      // ------------------------------------------------------
      public Idle(AI owner, StateMachine machine)
      {
         this.owner = owner;
         this.machine = machine;
      }

      // ------------------------------------------------------
      // Updates
      // ------------------------------------------------------
      public override void OnUpdate()
      {
         idleTimeLeft -= Time.deltaTime;
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
         idleTimeLeft = idleTime;
      }

      public override void OnExit()
      {
          
      }

      // ------------------------------------------------------
      // HandleStateChanges
      // ------------------------------------------------------
      public override void HandleStateChanges()
      {
         if (owner.TargetSighted() && idleTimeLeft <= 0)
         {
            machine.ChangeState(StateMachine.PURSUE);
         }
      }
   }
}
