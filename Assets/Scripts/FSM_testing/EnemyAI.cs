using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
   private EnemyStateMachine machine;

   public void OnUpdate(){
      machine.UpdateState();
   }

   public void OnFixedUpdate(){
      machine.FixedUpdateState();
   }
}
