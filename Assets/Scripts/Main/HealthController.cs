using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private float recoveryTime;
   private float recoveryTimeLeft = 0.0f;
   private GameController control;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public void OnStart(){
      control = FindObjectOfType<GameController>();
   }

   public void OnUpdate(Player player, Possessable possessedObject, List<Enemy> enemies){
      HandleEnemyContactDamage(possessedObject, enemies);
   }

   // ------------------------------------------------------
   // Damage 
   // ------------------------------------------------------
   private void HandleEnemyContactDamage(Possessable possessedObject, List<Enemy> enemies){
      foreach(Enemy enemy in enemies){
         if(enemy.Health.CollidingWithPlayer && !IsRecovering()){
            
            if(possessedObject != null){
               OnPossessedHit(possessedObject);
            } 
            else{
               PlayerDeath();
            }

            StartRecovery();
         }
      }
   }

   // ------------------------------------------------------
   // Recovery
   // ------------------------------------------------------
   private void HandlePlayerRecovery(){
      if(recoveryTimeLeft > 0.0f){
         recoveryTimeLeft -= Time.deltaTime;
      }
   }

   private void StartRecovery(){
      recoveryTimeLeft = recoveryTime;
   }

   private bool IsRecovering(){
      return (recoveryTimeLeft > 0.0f);
   }


   // ------------------------------------------------------
   // OnHit
   // ------------------------------------------------------
   private void OnPossessedHit(Possessable possessedObject){
      control.ForceUnpossession();
   }

   private void PlayerDeath(){
      Debug.Log("Player Killed");
      StartCoroutine(ReloadLevel());
   }

   private IEnumerator ReloadLevel(){
      Debug.Log("restarting level in 3 seconds");
      yield return new WaitForSeconds(3);
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
