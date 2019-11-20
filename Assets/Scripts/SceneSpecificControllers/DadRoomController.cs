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

   [SerializeField] private Vector2 throwPlayerBack;
   [SerializeField] private float rateOfSlow;

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
      if(Input.GetKeyDown(KeyCode.Space)){
         BeginEnemyEncounter(enemyController, control);
      }
      if(Input.GetKeyDown(KeyCode.J)){
         
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

   private void DadExit()
   {
      exit.Unlock();
      exit.Open();

      // TODO: add a wait time here
      Destroy(drunkDad);

      // TODO: add a wait time here
      exit.Close();
   }

   private void BeginEnemyEncounter(EnemyController enemyController, GameController control)
   {
      control.PossessionController.ForcedUnpossession(control.Player, throwPlayerBack, rateOfSlow);

      enemy1 = enemyController.SpawnEnemy(groundEnemyPrefab, enemy1SpawnPosition);
      enemy2 = enemyController.SpawnEnemy(groundEnemyPrefab, enemy2SpawnPosition);

      enemy1.AddImpulse(new Vector2(-10, 10), 0.05f);
      enemy2.AddImpulse(new Vector2(-10, 10), 0.05f);

      List<Enemy> list = new List<Enemy>();
      list.Add(enemy1);
      list.Add(enemy2);

      enemyController.SpawnEnemyWall(enemyWallPrefab, list, wallSpawnPosition, new Vector3(2, 29, 1));
   }

   // ------------------------------------------------------
   // Private methods
   // ------------------------------------------------------
   private Vector3 GetHorizontalTarget(MonoBehaviour target)
   {
      return new Vector3(target.transform.position.x, drunkDad.transform.position.y, 0);
   }
}
