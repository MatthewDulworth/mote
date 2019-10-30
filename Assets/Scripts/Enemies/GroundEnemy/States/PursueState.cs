using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : State<GroundEnemyAI>
{
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

   }
   public override void OnFixedUpdate(){

   }

   // ------------------------------------------------------
   // State Changes
   // ------------------------------------------------------
   public override void HandleStateChanges(){
      if(!owner.AttackOnCooldown() && owner.TargetInRange() && owner.OnGround()){
         machine.ChangeState(GE_StateMachine.ATTACK);
      }
      else if(!owner.TargetSighted()){
         machine.ChangeState(GE_StateMachine.PATROL);
      }
   }
}
