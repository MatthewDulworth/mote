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
    
   [SerializeField] private float range;
   [SerializeField] private float movementSpeed;
   [SerializeField] private float diagonalLimiter;
   

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void OnValidate(){
      range = Mathf.Max(range, 0);
      movementSpeed = Mathf.Max(movementSpeed, 0);
      diagonalLimiter = Mathf.Max(diagonalLimiter, 0);
   }

   void Start() {
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

   public float Range{
      get{return range;}
   }
}
