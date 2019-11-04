using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private List<Possessable> possessables;
   private List<Enemy> enemies;

   [SerializeField] private PossessionController possessionController;
   [SerializeField] private Player player;
   [SerializeField] private InputController io;
   [SerializeField] private LayerMask playerLayer;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start() {
      possessables = new List<Possessable>();
      enemies = new List<Enemy>();

      Possessable[] pos = FindObjectsOfType<Possessable>();
      foreach(Possessable obj in pos){
         possessables.Add(obj);
      }

      Enemy[] enem = FindObjectsOfType<Enemy>();
      foreach(Enemy enemy in enem){
         enemies.Add(enemy);
      }

      possessionController.OnStart(possessables);
   }

   void Update() {
      player.OnUpdate();
      possessionController.OnUpdate(player, io);

      HandleEnemyUpdates();
      HandlePlayerDamageFromEnemies();
   }

   void FixedUpdate(){
      HandleEnemyFixedUpdates();

      if(possessionController.CurrentlyPossessing()){
         possessionController.PossessedObject.OnFixedUpdate(io);
      }
      else {
         player.HandleMovement(io);
      }
   }
   
   // ------------------------------------------------------
   // Private Enemy Methods
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

   private void HandlePlayerDamageFromEnemies(){
      foreach(Enemy enemy in enemies){
         if(enemy.Health.CollidingWithPlayer){
            enemy.OnDamageCollisonPlayer(player);
         }
      }
   }
   
   
   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void ForceUnpossession(){
      possessionController.ForcedUnpossession(player);
   }
}
