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
      states = new List<State<GroundEnemyAI>>();

      this.owner = owner;

      states.Add(new PatrolState(owner, this));
      states.Add(new PursueState(owner, this));
      states.Add(new AttackState(owner, this));

      ChangeState(PATROL);
   }
}
