using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadRoomController : SceneSpecificController
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private GameObject groundEnemyPrefab;
   [SerializeField] private GameObject enemyWallPrefab;
   [SerializeField] private GameObject drunkDad;
   [SerializeField] private float dadSpeed;
   [SerializeField] private Vector3 enemy1SpawnPosition;
   [SerializeField] private Vector3 enemy2SpawnPosition;
   [SerializeField] private Vector3 wallSpawnPosition;

   private Enemy enemy1;
   private Enemy enemy2;
   private p_FrontFacingDoor exit;

   private bool dadMoveTrigger = false;
   private bool yeet = true;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void OnStart(EnemyController enemyController)
   {
      exit = FindObjectOfType<p_FrontFacingDoor>();
   }

   public override void OnUpdate(EnemyController enemyController)
   {
      // Debugging
      if (Input.GetKeyDown(KeyCode.Space))
      {
         dadMoveTrigger = true;
      }
      if (Input.GetKeyDown(KeyCode.LeftCommand))
      {
         if (yeet)
         {
            enemyController.DestroyEnemy(enemy1);
            yeet = false;
         }
         else
         {
            enemyController.DestroyEnemy(enemy2);
         }
      }
      HandleDadEncounter(enemyController);
   }

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void BeginEnemyEncounter(EnemyController enemyController)
   {
      enemy1 = enemyController.SpawnEnemy(groundEnemyPrefab, enemy1SpawnPosition);
      enemy2 = enemyController.SpawnEnemy(groundEnemyPrefab, enemy2SpawnPosition);

      List<Enemy> list = new List<Enemy>();
      list.Add(enemy1);
      list.Add(enemy2);

      enemyController.SpawnEnemyWall(enemyWallPrefab, list, wallSpawnPosition, new Vector3(2, 29, 1));
   }

   private void HandleDadEncounter(EnemyController enemyController)
   {
      if (dadMoveTrigger == true && drunkDad != null)
      {
         Vector3 target = new Vector3(exit.transform.position.x, drunkDad.transform.position.y, 0);
         drunkDad.transform.position = Vector2.MoveTowards(drunkDad.transform.position, target, dadSpeed * Time.deltaTime);

         if (drunkDad.transform.position == target)
         {
            DadExit(enemyController);
         }
      }
   }

   private void DadExit(EnemyController enemyController)
   {
      exit.Unlock();
      exit.Open();

      // TODO: add a wait time here
      Destroy(drunkDad);

      // TODO: add a wait time here
      exit.Close();
      BeginEnemyEncounter(enemyController);
   }
}
