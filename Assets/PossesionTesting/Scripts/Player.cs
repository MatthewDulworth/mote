using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Rigidbody2D rb;
   [SerializeField] private float movementSpeed;
   [SerializeField] private float diagonalLimiter;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start() {
      rb = GetComponent<Rigidbody2D>();
   }

   // ------------------------------------------------------
   // Player Methods
   // ------------------------------------------------------
   public void HandleMovement(InputController io){
      float horizontal = io.GetHorizontalDirection();
      float vertical = io.getVerticalDirection();

      if(horizontal != 0 && vertical !=0){
         horizontal *= diagonalLimiter;
         vertical *= diagonalLimiter;
      }

      rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
   }

   public void StopMoving(){
      rb.velocity = Vector2.zero;
   }
}
