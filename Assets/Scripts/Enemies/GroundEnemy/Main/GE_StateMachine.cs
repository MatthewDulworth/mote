using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GE_StateMachine : StateMachine<GroundEnemyAI>
{
   // ------------------------------------------------------
   // constants
   // ------------------------------------------------------
   public static int PATROL = 0;
   public static int PURSUE = 1;
   public static int FALL = 2;

   // ------------------------------------------------------
   // constructor
   // ------------------------------------------------------
   public GE_StateMachine(GroundEnemyAI owner){
      states = new List<State<GroundEnemyAI>>();

      this.owner = owner;

      states.Add(new PatrolState(owner, this));
      states.Add(new PursueState(owner, this));
      states.Add(new FallState(owner, this));

      ChangeState(PATROL);
   }
}
