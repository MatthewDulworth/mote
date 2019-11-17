using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWall : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private List<Enemy> enemies;

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void Init(List<Enemy> enemies)
   {
      this.enemies = enemies;
   }

   public List<Enemy> Enemies
   {
      get { return enemies; }
   }
}
