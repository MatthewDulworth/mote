using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneSpecificController : MonoBehaviour
{
   public virtual void OnStart(EnemyController enemyControl) { }
   public virtual void OnUpdate(EnemyController enemyControl) { }
}
