using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<OwnerType>
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private State<OwnerType> currentState;
   private List<State<OwnerType>> states;
   private OwnerType parent;

   // ------------------------------------------------------
   // Update
   // ------------------------------------------------------
   public void OnUpdate(){
      currentState.OnUpdate();
   }

   public void OnFixedUpdate(){
      currentState.OnFixedUpdate();
   }

   // ------------------------------------------------------
   // Change State
   // ------------------------------------------------------
   public void ChangeState(int index){

      if(index >= states.Count || index < 0){
         Debug.LogErrorFormat("cannot change states, invalid index: {0}", index);
      }

      if(currentState != null){
         currentState.OnExit();
      }

      currentState = states[index];
      currentState.OnEnter();
   }
}
