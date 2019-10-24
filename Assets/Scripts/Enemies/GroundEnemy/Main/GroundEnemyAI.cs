using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAI : Enemy
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private StateMachine<GroundEnemyAI> machine;

   // ------------------------------------------------------
   // Update
   // ------------------------------------------------------
   public override void OnStart(){
      machine = new GE_StateMachine(this);
   }
   public override void OnFixedUpdate(){}
   public override void OnUpdate(){}
}
