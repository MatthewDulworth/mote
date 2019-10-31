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
      // nada
   }
   public override void OnFixedUpdate(){
      HandleMovement();
   }

   private void HandleMovement(){
      Debug.Log("yeet");
      owner.ChangeVelocityScaled(1,0);

      if(owner.WallDetected() || owner.EdgeDetected()){
         owner.FlipHorizontal();
      }
   }

   // ------------------------------------------------------
   // State Changes
   // ------------------------------------------------------
   public override void HandleStateChanges(){
      if(owner.TargetInRange() && owner.OnGround() && !owner.AttackOnCooldown()){
         machine.ChangeState(GE_StateMachine.ATTACK);
      }
      else if(owner.TargetSighted()){
         machine.ChangeState(GE_StateMachine.PURSUE);
      }
   }
}
