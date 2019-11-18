using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadRoomController : SceneSpecificController
{
   [SerializeField] private GameObject groundEnemyPrefab;
   [SerializeField] private GameObject enemyWallPrefab;
   [SerializeField] private Vector3 enemy1SpawnPosition;
   [SerializeField] private Vector3 enemy2SpawnPosition;
   [SerializeField] private Vector3 wallSpawnPosition;

   private Enemy enemy1;
   private Enemy enemy2;

   public override void OnStart(EnemyController enemyControl)
   {

   }

   public override void OnUpdate(EnemyController enemyControl)
   {
      if(Input.GetKeyDown(KeyCode.Space)){
         BeginEnemyEncounter(enemyControl);
      }
      if(Input.GetKeyDown(KeyCode.LeftCommand)){
         Debug.Log("yeet");
         enemyControl.DestroyEnemy(enemy1);
         enemyControl.DestroyEnemy(enemy2);
      }
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
}
