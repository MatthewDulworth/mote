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
   [SerializeField] private GameObject drunkDad;
   [SerializeField] private GameObject fallWall;

   [SerializeField] private float dadSpeed;
   [SerializeField] private float rateOfSlow;
   [SerializeField] private Vector2 throwPlayerBack;

   private Enemy enemy1;
   private Enemy enemy2;
   private p_FrontFacingDoor exit;
   private p_TV tv;
   private MonoBehaviour target;

   private bool dadMoveTrigger = false;
   private bool enemySpawnTrigger = false;
   private bool yeet = true;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void OnStart(EnemyController enemyController)
   {
      exit = FindObjectOfType<p_FrontFacingDoor>();
      tv = FindObjectOfType<p_TV>();
   }

   public override void OnUpdate(EnemyController enemyController, GameController control)
   {
      // Debugging
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

      if (Input.GetKeyDown(KeyCode.Space))
      {
         BeginEnemyEncounter(enemyController, control);
      }

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
      target = GetDadTarget();

      if (dadMoveTrigger == true && drunkDad != null)
      {
         drunkDad.transform.position = Vector2.MoveTowards(drunkDad.transform.position, GetHorizontalTarget(target), dadSpeed * Time.deltaTime);

         if (drunkDad.transform.position == GetHorizontalTarget(exit) && target == exit)
         {
            DadExit();
         }
         else if (drunkDad.transform.position == GetHorizontalTarget(tv) && target == tv)
         {
            tv.PowerOff();
         }
      }
   }

   private MonoBehaviour GetDadTarget()
   {
      MonoBehaviour target = null;
      if (tv.IsOn)
      {
         if (!dadMoveTrigger)
         {
            dadMoveTrigger = true;
         }
         target = tv;
      }
      else
      {
         target = exit;
      }
      return target;
   }

   private void DadExit()
   {
      exit.Unlock();
      exit.Open();

      // TODO: add a wait time here
      Destroy(drunkDad);

      // TODO: add a wait time here
      exit.Close();
      Destroy(fallWall);
   }

   private void BeginEnemyEncounter(EnemyController enemyController, GameController control)
   {
      control.PossessionController.ForcedUnpossession(control.Player, throwPlayerBack, rateOfSlow);

      Vector3 enemy1Spawn = new Vector3(exit.transform.position.x + 5, exit.transform.position.y, 0);
      Vector3 enemy2Spawn = new Vector3(exit.transform.position.x, exit.transform.position.y - 1, 0);

      enemy1 = enemyController.SpawnEnemy(enemy1Prefab, enemy1Spawn);
      enemy2 = enemyController.SpawnEnemy(enemy2Prefab, enemy2Spawn);

      enemy1.AddImpulse(new Vector2(5, 20), 0.05f);
      enemy2.AddImpulse(new Vector2(0, 20), 0.05f);

      StartCoroutine(SpawnWall(enemyController));
   }

   private IEnumerator SpawnWall(EnemyController enemyController){

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
