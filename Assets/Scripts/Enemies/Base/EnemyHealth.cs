using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   private float health;
   private Enemy parentEnemy;
   private BoxCollider2D EnemyCollider;
   private bool collidingWithPlayer;

   [SerializeField] private float maxHealth = 1;
   [SerializeField] private float recoveryTime = 0;

   void Start(){
      parentEnemy = GetComponentInParent<Enemy>();
      health = maxHealth;

      if(parentEnemy == null){
         Debug.LogError("This enemy health does not have a parent");
      }
   }

   // ------------------------------------------------------
   // Health
   // ------------------------------------------------------
   public void TakeDamage(float damage){
      health -= damage;
      health = Mathf.Max(health, 0.0f);
   }

   public void Heal(float heal){
      health += heal;
      health = Mathf.Min(health, maxHealth);
   }

   // ------------------------------------------------------
   // Collisions
   // ------------------------------------------------------
   void OnTriggerEnter2D(Collider2D collider){
      if(collider.tag == "Player"){
         collidingWithPlayer = true;
      }
   }

   void OnTriggerExit2D(Collider2D collider){
      if(collider.tag == "Player"){
         collidingWithPlayer = false;
      }
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public bool CollidingWithPlayer{
      get{return collidingWithPlayer;}
   }

}
