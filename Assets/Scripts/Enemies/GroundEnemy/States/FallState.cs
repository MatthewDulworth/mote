using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : State<GroundEnemyAI>
{
   // ------------------------------------------------------
   // Constructor
   // ------------------------------------------------------
   public FallState(GroundEnemyAI owner, GE_StateMachine machine){
      this.machine = machine;
      this.owner = owner;
   }

   // ------------------------------------------------------
   // Updates
   // ------------------------------------------------------
   public override void OnUpdate(){
      // nada
   }
   public override void OnFixedUpdate(){
      // nada
   }

   // ------------------------------------------------------
   // State Changes
   // ------------------------------------------------------
   public override void HandleStateChanges(){
      if(owner.OnGround()){
         machine.ChangeState(GE_StateMachine.PATROL);
      }
   }

   public override void OnEnter(){

   }
}
