using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private List<Enemy> enemies;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public void OnStart(){
      enemies = new List<Enemy>();

      Enemy[] enem = FindObjectsOfType<Enemy>();
      foreach(Enemy enemy in enem){
         enemies.Add(enemy);
      }
   }

   public void OnUpdate(){
      HandleEnemyUpdates();
   }

   public void OnFixedUpdate(){
      HandleEnemyFixedUpdates();
   }


   // ------------------------------------------------------
   // Enemy Updates
   // ------------------------------------------------------
   private void HandleEnemyUpdates(){
      foreach(Enemy enemy in enemies){
         enemy.OnUpdate();
      }
   }

   private void HandleEnemyFixedUpdates(){
      foreach(Enemy enemy in enemies){
         enemy.OnFixedUpdate();
      }
   }


   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public List<Enemy> Enemies{
      get{return enemies;}
   }
}
