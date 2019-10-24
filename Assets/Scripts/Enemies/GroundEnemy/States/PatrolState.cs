using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State<GroundEnemyAI>
{
   public PatrolState(GroundEnemyAI owner){
      this.owner = owner;
   }
   
   public override void OnEnter(){}
   public override void OnExit(){}
   public override void OnUpdate(){}
   public override void OnFixedUpdate(){}
}
