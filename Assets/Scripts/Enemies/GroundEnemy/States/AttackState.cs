﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<GroundEnemyAI>
{
   // ------------------------------------------------------
   // Constructor
   // ------------------------------------------------------
   public AttackState(GroundEnemyAI owner){
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

   }
}
