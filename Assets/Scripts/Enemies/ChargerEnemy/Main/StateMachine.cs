using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargerEnemy
{
   public class StateMachine : StateMachine<AI>
   {
      // ------------------------------------------------------
      // Member Vars
      // ------------------------------------------------------
      public static int IDLE = 0;
      public static int PURSUE = 1;
      public static int CHARGE = 2;

      // ------------------------------------------------------
      // Constructor
      // ------------------------------------------------------
      public StateMachine(AI owner)
      {
         states = new List<State<AI>>();
         this.owner = owner;

         states.Add(new Idle(owner, this));
         states.Add(new Pursue(owner, this));
         states.Add(new Charge(owner, this));
      }
   }
}

