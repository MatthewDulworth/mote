using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Rigidbody2D rb;
   private InputController io;
   private PossessableObject currentPosseableObject;
   [SerializeField] private float movementSpeed;
   [SerializeField] private float diagonalLimiter;
   

   // ------------------------------------------------------
   // Mono
   // ------------------------------------------------------
   void Start() {
      rb = GetComponent<Rigidbody2D>();
      io = GetComponent<InputController>();
      currentPosseableObject = null;
   }

   void Update(){
      // HandleAction();
   }

   void FixedUpdate(){
      HandleMovement();
   }

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void HandleMovement(){
      if(currentPosseableObject != null){
         currentPosseableObject.HandleMovement();
      } 
      else {
         PlayerMovement();
      }
   }

   private void PlayerMovement(){

      float horizontal = io.GetHorizontalDirection();
      float vertical = io.getVerticalDirection();

      if(horizontal != 0 && vertical !=0){
         horizontal *= diagonalLimiter;
         vertical *= diagonalLimiter;
      }

      rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
   }

   private void HandleAction(){
      if(currentPosseableObject != null){
         currentPosseableObject.HandleAction();
      }
      else{
         // do any player actions here
      }
   }

   private void PossessObject(PossessableObject obj){
      if(currentPosseableObject != null){
         currentPosseableObject.OnPossessedExit();
      }
      currentPosseableObject = obj;
      currentPosseableObject.OnPossessedEnter();
   }
}
