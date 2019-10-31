using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomEditorMono : MonoBehaviour
{
   public bool DisplayState;
   public Enemy enemy;

   public bool FindEnemy(){
      enemy = GetComponent<Enemy>();
      return (enemy != null);
   }
}
