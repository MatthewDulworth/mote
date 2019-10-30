using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State<GroundEnemyAI>
{
   // ------------------------------------------------------
   // Constructor
   // ------------------------------------------------------
   public PatrolState(GroundEnemyAI owner, GE_StateMachine machine){
      this.machine = machine;
      this.owner = owner;
   }

   // ------------------------------------------------------
   // Updates
   // ------------------------------------------------------
   public override void OnUpdate(){
      HandleStateChanges();
   }
   public override void OnFixedUpdate(){
      
   }

   // ------------------------------------------------------
   // State Changes
   // ------------------------------------------------------
   public override void HandleStateChanges(){
      if(owner.TargetInRange() && owner.OnGround()){
         machine.ChangeState(GE_StateMachine.ATTACK);
      }
      else if(owner.TargetSighted()){
         machine.ChangeState(GE_StateMachine.PURSUE);
      }
   }
}
