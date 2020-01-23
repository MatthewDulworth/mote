using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadRoomController : SceneSpecificController
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private GameObject enemy1Prefab;
   [SerializeField] private GameObject enemy2Prefab;
   [SerializeField] private GameObject enemyWallPrefab;
   [SerializeField] private GameObject fallWall;

   [SerializeField] private float rateOfSlow;
   [SerializeField] private Vector2 throwPlayerBack;

   private Dad drunkDad;
   private Enemy enemy1;
   private Enemy enemy2;
   private p_FrontFacingDoor exit;
   private p_TV tv;
  
   private bool enemySpawnTrigger = false;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void OnStart(EnemyController enemyController)
   {
      exit = FindObjectOfType<p_FrontFacingDoor>();
      tv = FindObjectOfType<p_TV>();
      drunkDad = FindObjectOfType<Dad>();
      drunkDad.init(tv, exit);
   }

   public override void OnUpdate(EnemyController enemyController, GameController control)
   {
      if (!enemySpawnTrigger)
      {
         if (!exit.IsClosed)
         {
            enemySpawnTrigger = true;
            BeginEnemyEncounter(enemyController, control);
         }
         HandleDadEncounter();
      }
   }

   // ------------------------------------------------------
   // Dad Encounter
   // ------------------------------------------------------
   private void HandleDadEncounter()
   {
      drunkDad.move();
   }

   public void DadExit()
   {
      exit.Unlock();
      exit.Open();
      Destroy(drunkDad);
   }

   public void CloseDoor()
   {
      exit.Close();
      Destroy(fallWall);
   }

   private void BeginEnemyEncounter(EnemyController enemyController, GameController control)
   {
      AudioManager.Instance.Play("Meglovania");
      control.PossessionController.ForcedUnpossession(control.Player, throwPlayerBack, rateOfSlow);

      Vector3 enemy1Spawn = new Vector3(exit.transform.position.x + 5, exit.transform.position.y, 0);
      Vector3 enemy2Spawn = new Vector3(exit.transform.position.x, exit.transform.position.y - 1, 0);

      enemy1 = enemyController.SpawnEnemy(enemy1Prefab, enemy1Spawn);
      enemy2 = enemyController.SpawnEnemy(enemy2Prefab, enemy2Spawn);

      enemy1.AddImpulse(new Vector2(5, 20), 0.05f);
      enemy2.AddImpulse(new Vector2(0, 20), 0.05f);

      StartCoroutine(SpawnWall(enemyController));
   }

   private IEnumerator SpawnWall(EnemyController enemyController)
   {
      yield return new WaitForSeconds(0.5f);

      List<Enemy> list = new List<Enemy>();
      list.Add(enemy1);
      list.Add(enemy2);

      EnemyWall wall = enemyController.SpawnEnemyWall(enemyWallPrefab, list, exit.transform.position, exit.transform.localScale);
   }

   // ------------------------------------------------------
   // Private methods
   // ------------------------------------------------------
   private Vector3 GetHorizontalTarget(MonoBehaviour target)
   {
      return new Vector3(target.transform.position.x, drunkDad.transform.position.y, 0);
   }
}
