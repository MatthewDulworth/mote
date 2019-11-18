using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadRoomController : SceneSpecificController
{
   [SerializeField] private GameObject groundEnemyPrefab;
   [SerializeField] private GameObject enemyWallPrefab;
   [SerializeField] private GameObject drunkDad;
   [SerializeField] private Vector3 enemy1SpawnPosition;
   [SerializeField] private Vector3 enemy2SpawnPosition;
   [SerializeField] private Vector3 wallSpawnPosition;

   private Enemy enemy1;
   private Enemy enemy2;
   private p_FrontFacingDoor exit;

   private bool dadMoveTrigger = false;

   public override void OnStart(EnemyController enemyControl)
   {
      exit = FindObjectOfType<p_FrontFacingDoor>();
   }

   public override void OnUpdate(EnemyController enemyControl)
   {
      // Debugging
      if (Input.GetKeyDown(KeyCode.Space))
      {
         // BeginEnemyEncounter(enemyControl);
         dadMoveTrigger = true;
      }
      if (Input.GetKeyDown(KeyCode.LeftCommand))
      {
         // enemyControl.DestroyEnemy(enemy1);
         // enemyControl.DestroyEnemy(enemy2);
      }

      HandleDadMovement();
   }

   private void BeginEnemyEncounter(EnemyController enemyControl)
   {
      enemy1 = enemyControl.SpawnEnemy(groundEnemyPrefab, enemy1SpawnPosition);
      enemy2 = enemyControl.SpawnEnemy(groundEnemyPrefab, enemy2SpawnPosition);

      List<Enemy> list = new List<Enemy>();
      list.Add(enemy1);
      list.Add(enemy2);

      enemyControl.SpawnEnemyWall(enemyWallPrefab, list, wallSpawnPosition, new Vector3(2, 29, 1));
   }

   private void HandleDadMovement(){
      if (dadMoveTrigger == true && drunkDad != null)
      {
         Vector3 target = new Vector3(exit.transform.position.x, drunkDad.transform.position.y, 0);
         drunkDad.transform.position = Vector2.MoveTowards(drunkDad.transform.position, target, 3*Time.deltaTime);

         if(drunkDad.transform.position == target){
            Destroy(drunkDad);
         }
      }
   }
}
