using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargerEnemy
{
   public class AI : Enemy
   {
      // ------------------------------------------------------
      // Member Vars
      // ------------------------------------------------------
      private StateMachine<AI> machine;

      // ------------------------------------------------------
      // Start
      // ------------------------------------------------------
      public override void OnValidate(){
         base.OnValidate();
      }

      public override void OnStart()
      {
         machine = new StateMachine(this);
         machine.ChangeState(StateMachine.IDLE);
      }

      // ------------------------------------------------------
      // Updates
      // ------------------------------------------------------
      public override void OnUpdate()
      {
         HandleTargeting();
         machine.OnStateUpdate();
         Debug.Log(GetCurrentStateName());
      }

      public override void OnFixedUpdate()
      {
         machine.OnStateFixedUpdate();
      }

      // ------------------------------------------------------
      // Getters
      // ------------------------------------------------------
      public override string GetCurrentStateName()
      {
         return machine.GetStateName();
      }
   }
}
