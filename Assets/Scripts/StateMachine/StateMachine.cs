using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<OwnerType>
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected State<OwnerType> currentState;
   protected State<OwnerType> previousState;
   protected List<State<OwnerType>> states;
   protected OwnerType owner;

   // ------------------------------------------------------
   // Update
   // ------------------------------------------------------
   public void OnStateUpdate(){
      currentState.OnUpdate();
      currentState.HandleStateChanges();
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
      previousState = currentState;

      currentState = states[index];
      currentState.OnEnter();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public string GetStateName(){
      return currentState.GetType().Name;
   }

   public State<OwnerType> GetState(int index){
      return states[index];
   }

   public State<OwnerType> PreviousState{
      get{return previousState;}
   }

   public State<OwnerType> CurrentState{
      get{return currentState;}
   }
}
