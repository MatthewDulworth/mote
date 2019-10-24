using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
   private EnemyState currentState;

   public void ChangeState(EnemyState _state){
      if(currentState != null){
         currentState.OnStateExit();
      }
      currentState = _state;
      currentState.OnStateExit();
   }

   public void UpdateState(){
      currentState.OnUpdate();
   }

   public void FixedUpdateState(){
      currentState.OnFixedUpdate();
   }
}
