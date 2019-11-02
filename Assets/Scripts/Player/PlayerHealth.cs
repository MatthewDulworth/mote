using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   private float health;
   private float recoveryTimeLeft;
   private BoxCollider2D EnemyCollider;

   [SerializeField] private float maxHealth = 1;
   [SerializeField] private float recoveryTime = 1;

   void Start(){
      health = maxHealth;
   }

   // ------------------------------------------------------
   // Health
   // ------------------------------------------------------
   public void TakeDamage(float damage){
      if(recoveryTimeLeft <= 0){
         health -= damage;
         health = Mathf.Max(health, 0.0f);
         recoveryTimeLeft = recoveryTime;

         Debug.Log(health);
      }
      else{
         Debug.Log("Recovering");
      }
   }

   public void HandleRecovery(){
      recoveryTimeLeft -= Time.deltaTime;
   }

   public void Heal(float heal){
      health += heal;
      health = Mathf.Min(health, maxHealth);
   }

   public bool Depleted(){
      return (health <= 0);
   }
}
