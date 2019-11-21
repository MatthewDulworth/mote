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
      private Vector3 target;

      // ------------------------------------------------------
      // Constructor
      // ------------------------------------------------------
      public Charge(AI owner, StateMachine machine){
         this.owner = owner;
         this.machine = machine;
      }

      // ------------------------------------------------------
      // Updates
      // ------------------------------------------------------
      public override void OnUpdate()
      {

      }

      public override void OnFixedUpdate()
      {

      }

      // ------------------------------------------------------
      // Enter/Exit
      // ------------------------------------------------------
      public override void OnEnter()
      {
         target = owner.CurrentTarget.position;
      }

      public override void OnExit()
      {

      }


      // ------------------------------------------------------
      // HandleStateChanges
      // ------------------------------------------------------
      public override void HandleStateChanges()
      {
         if(chargeDone)
         {
            machine.ChangeState(StateMachine.IDLE);
         }
      }
   }
}


