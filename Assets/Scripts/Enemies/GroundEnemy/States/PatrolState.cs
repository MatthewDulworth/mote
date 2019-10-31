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
      owner.ChangeVelocityScaled(1,0);

      if(owner.WallDetected() || owner.EdgeDetected()){
         owner.FlipHorizontal();
      }
   }

   // ------------------------------------------------------
   // State Changes
   // ------------------------------------------------------
   public override void HandleStateChanges(){
      
   }
}
