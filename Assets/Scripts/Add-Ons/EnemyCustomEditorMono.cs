using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomEditorMono : MonoBehaviour
{
   private Enemy enemy;

   public bool FindEnemy()
   {
      enemy = GetComponent<Enemy>();
      return (enemy != null);
   }

   public Enemy GetEnemy()
   {
      return enemy;
   }
}
