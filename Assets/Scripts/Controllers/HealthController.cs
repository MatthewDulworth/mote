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
   private float recoveryTimeLeft;
   private GameController control;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public void OnStart()
   {
      recoveryTimeLeft = 0.0f;
      control = FindObjectOfType<GameController>();
   }

   public void OnUpdate(Player player, Possessable possessedObject, List<Enemy> enemies)
   {
      HandleEnemyContactDamage(possessedObject, enemies);
      HandlePlayerRecovery();
   }

   // ------------------------------------------------------
   // Damage 
   // ------------------------------------------------------
   private void HandleEnemyContactDamage(Possessable possessedObject, List<Enemy> enemies)
   {
      foreach (Enemy enemy in enemies)
      {
         if (enemy.Health.CollidingWithPlayer && !IsRecovering())
         {

            if (possessedObject != null)
            {
               OnPossessedHit(possessedObject);
            }
            else
            {
               PlayerDeath();
            }

            StartRecovery();
         }
         if (IsRecovering())
         {
            Debug.Log("recovering");
         }
      }
   }

   // ------------------------------------------------------
   // Recovery
   // ------------------------------------------------------
   private void HandlePlayerRecovery()
   {
      if (IsRecovering())
      {
         recoveryTimeLeft -= Time.deltaTime;
      }
   }

   private void StartRecovery()
   {
      recoveryTimeLeft = recoveryTime;
   }

   private bool IsRecovering()
   {
      return (recoveryTimeLeft > 0.0f);
   }


   // ------------------------------------------------------
   // OnHit
   // ------------------------------------------------------
   private void OnPossessedHit(Possessable possessedObject)
   {
      Debug.Log("hit");
      control.PossessionController.ForcedUnpossession(control.Player);
   }

   private void PlayerDeath()
   {
      Debug.Log("Player Killed");
      StartCoroutine(ReloadLevel());
   }

   private IEnumerator ReloadLevel()
   {
      Debug.Log("restarting level in 1 second");
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
