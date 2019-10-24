using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GE_StateMachine : StateMachine<GroundEnemyAI>
{
   // ------------------------------------------------------
   // constants
   // ------------------------------------------------------
   public const int PATROL = 0;
   public const int PURSUE = 1;
   public const int ATTACK = 2;

   // ------------------------------------------------------
   // constructor
   // ------------------------------------------------------
   public GE_StateMachine(GroundEnemyAI owner){
      this.owner = owner;

      states.Add(new PatrolState());
      states.Add(new PursueState());
      states.Add(new AttackState());

      ChangeState(PATROL);
   }
}
