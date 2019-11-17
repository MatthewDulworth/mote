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
   public Enemy SpawnEnemy(GameObject enemyPrefab, float x, float y)
   {
      if (enemyPrefab.GetComponent<Enemy>())
      {
         Enemy enemy = Instantiate(enemyPrefab.GetComponent<Enemy>());
         enemies.Add(enemy);
         enemy.transform.position = new Vector3(x, y, 0);
         return enemy;
      }
      else
      {
         Debug.LogError("Enemy controller cannot spawn non-enemy prefabs");
         return null;
      }
   }

   public void SpawnEnemyWall(GameObject enemyWallPrefab, List<Enemy> enemies, float x, float y)
   {
      if (enemyWallPrefab.GetComponent<EnemyWall>())
      {
         EnemyWall enemyWall =  Instantiate(enemyWallPrefab).GetComponent<EnemyWall>();
         enemyWalls.Add(enemyWall);
         enemyWall.transform.position = new Vector3(x, y, 0);
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
      Destroy(enemy);
   }

   public void DestroyEnemyWall(EnemyWall wall)
   {
      enemyWalls.Remove(wall);
      Destroy(wall.gameObject);
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
