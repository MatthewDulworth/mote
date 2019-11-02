using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Rigidbody2D rb;
   private PlayerHealth playerHealth;
   private Possession possession;

   [SerializeField] private float movementSpeed;
   [SerializeField] private float diagonalLimiter;
   

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start() {
      possession = GetComponent<Possession>();
      playerHealth = gameObject.GetComponentInChildren<PlayerHealth>();
      rb = GetComponent<Rigidbody2D>();
   }

   public void OnUpdate(){
      Health.HandleRecovery();
   }

   public void PlayerDeath(){
      Debug.Log("Player Killed!");
   }

   // ------------------------------------------------------
   // Movement
   // ------------------------------------------------------
   public void HandleMovement(InputController io){
      float horizontal = io.GetHorizontalDirection();
      float vertical = io.GetVerticalDirection();

      if(horizontal != 0 && vertical !=0){
         horizontal *= diagonalLimiter;
         vertical *= diagonalLimiter;
      }

      rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
   }

   public void StopMoving(){
      rb.velocity = Vector2.zero;
   }

   public void MoveTo(Vector3 pos){
      transform.position = pos;
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public Rigidbody2D RB{
      get{return rb;}
   }

   public PlayerHealth Health{
      get{return playerHealth;}
   }

   public Possession Possession{
      get{return Possession;}
   }
}
