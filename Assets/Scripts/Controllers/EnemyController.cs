using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private List<Enemy> enemies;
   private List<EnemyWall> enemyWalls;

   // enemy prefabs
   public GameObject GroundEnemy;


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public void OnStart()
   {
      enemies = new List<Enemy>();
      enemyWalls = new List<EnemyWall>();

      Enemy[] enem = FindObjectsOfType<Enemy>();
      foreach (Enemy enemy in enem)
      {
         enemies.Add(enemy);
      }

      EnemyWall[] walls = FindObjectsOfType<EnemyWall>();
      foreach (EnemyWall wall in walls)
      {
         enemyWalls.Add(wall);
      }
   }

   public void OnUpdate()
   {
      HandleBottleDamage();
      HandleEnemyUpdates();
      HandleEnemyWalls();
   }

   public void OnFixedUpdate()
   {
      HandleEnemyFixedUpdates();
   }


   // ------------------------------------------------------
   // Enemy Updates
   // ------------------------------------------------------
   private void HandleEnemyUpdates()
   {
      foreach (Enemy enemy in enemies)
      {
         enemy.OnUpdate();
      }
   }

   private void HandleEnemyFixedUpdates()
   {
      foreach (Enemy enemy in enemies)
      {
         enemy.OnFixedUpdate();
      }
   }

   private void HandleEnemyWalls()
   {
      foreach (EnemyWall wall in enemyWalls)
      {
         if (WallEnemiesAreDead(wall))
         {
            DestroyEnemyWall(wall);
         }
      }
   }


   // ------------------------------------------------------
   // Spawners
   // ------------------------------------------------------
   public Enemy SpawnEnemy(GameObject enemyPrefab, Vector3 position)
   {
      if (enemyPrefab.GetComponent<Enemy>())
      {
         Enemy enemy = Instantiate(enemyPrefab.GetComponent<Enemy>());
         enemies.Add(enemy);
         enemy.transform.position = position;
         return enemy;
      }
      else
      {
         Debug.LogError("Enemy controller cannot spawn non-enemy prefabs");
         return null;
      }
   }

   public void SpawnEnemyWall(GameObject enemyWallPrefab, List<Enemy> enemies, Vector3 position, Vector3 scale)
   {
      if (enemyWallPrefab.GetComponent<EnemyWall>())
      {
         EnemyWall enemyWall = Instantiate(enemyWallPrefab).GetComponent<EnemyWall>();
         enemyWalls.Add(enemyWall);
         enemyWall.transform.position = position;
         enemyWall.transform.localScale = scale;
         enemyWall.GetComponent<EnemyWall>().Init(enemies);
      }
      else
      {
         Debug.LogError("Enemy controller cannot spawn non-enemy prefabs");
      }
   }

   public void DestroyEnemy(Enemy enemy)
   {
      enemies.Remove(enemy);
      Destroy(enemy.gameObject);
   }

   public void DestroyEnemyWall(EnemyWall wall)
   {
      enemyWalls.Remove(wall);
      Destroy(wall.gameObject);
   }

   // ------------------------------------------------------
   // Take Damage
   // ------------------------------------------------------
   private void HandleBottleDamage()
   {
      for(int i=0; i<enemies.Count; i++)
      {
         if (enemies[i].HitBox.IsCollidingWith("BeerBottle"))
         {
            p_BeerBottle bottle = enemies[i].HitBox.GetCollidingObject("BeerBottle").GetComponent<p_BeerBottle>();
   
            if(bottle.AtDamageSpeed())
            {
               DestroyEnemy(enemies[i]);
            }
         }
      }
   }

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private bool WallEnemiesAreDead(EnemyWall wall)
   {
      foreach (Enemy enemy in wall.Enemies)
      {
         if (enemies.Contains(enemy))
         {
            return false;
         }
      }

      return true;
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public List<Enemy> Enemies
   {
      get { return enemies; }
   }
}
