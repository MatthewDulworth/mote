using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private List<Enemy> enemies;
   private Player player;

   private PossessionController possessControl;
   private InputController io;
   private HealthController healthController;

   [SerializeField] private LayerMask playerLayer;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start() {
      possessControl = FindObjectOfType<PossessionController>();
      player = FindObjectOfType<Player>();
      io = FindObjectOfType<InputController>();
      healthController = FindObjectOfType<HealthController>();

      enemies = new List<Enemy>();

      Enemy[] enem = FindObjectsOfType<Enemy>();
      foreach(Enemy enemy in enem){
         enemies.Add(enemy);
      }

      possessControl.OnStart();
      healthController.OnStart();
   }

   void Update() {
      possessControl.OnUpdate(player, io);

      HandleEnemyUpdates();
      healthController.OnUpdate(player, possessControl.PossessedObject(), enemies);

      if(possessControl.CurrentlyPossessing()){
         possessControl.PossessedObject().OnUpdate(io);
      } 
      else {
         player.OnUpdate();
      }
   }

   void FixedUpdate(){
      HandleEnemyFixedUpdates();

      if(possessControl.CurrentlyPossessing()){
         possessControl.PossessedObject().OnFixedUpdate(io);
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
   
   
   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void ForceUnpossession(){
      possessControl.ForcedUnpossession(player);
   }
}
