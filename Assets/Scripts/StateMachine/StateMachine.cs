using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<OwnerType>
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected State<OwnerType> currentState;
   protected List<State<OwnerType>> states;
   protected OwnerType owner;

   // ------------------------------------------------------
   // Update
   // ------------------------------------------------------
   public void OnStateUpdate(){
      currentState.OnUpdate();
   }

   public void OnStateFixedUpdate(){
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

      Debug.Log(currentState.GetType());
   }
}
