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
         HandleStateChanges();
      }

      public override void OnFixedUpdate()
      {
         
      }

      // ------------------------------------------------------
      // HandleStateChanges
      // ------------------------------------------------------
      public override void HandleStateChanges()
      {
         if (owner.TargetSighted())
         {
            machine.ChangeState(StateMachine.PURSUE);
         }
      }
   }
}
