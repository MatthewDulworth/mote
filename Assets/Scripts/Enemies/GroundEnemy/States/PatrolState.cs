using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State<GroundEnemyAI>
{
   // ------------------------------------------------------
   // Constructor
   // ------------------------------------------------------
   public PatrolState(GroundEnemyAI owner){
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
   public override void OnEnter(){
   
   }
   public override void OnExit(){

   } 
}
