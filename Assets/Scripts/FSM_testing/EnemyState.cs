using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
   protected EnemyState enemy;

   public abstract void OnStateEnter();
   public abstract void OnStateExit();
   public abstract void OnFixedUpdate();
   public abstract void OnUpdate();
}
