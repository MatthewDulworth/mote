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
   

   // ------------------------------------------------------
   // Mono
   // ------------------------------------------------------
   void Start() {
      rb = GetComponent<Rigidbody2D>();
      io = GetComponent<InputController>();
      currentPosseableObject = null;
   }

   void Update(){
      HandleAction();
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

      float horizontal = io.NONE;
      if(io.RightKeyHeld){
         horizontal += io.RIGHT;
      }
      if(io.LeftKeyHeld){
         horizontal += io.LEFT;
      }

      float vertical = io.NONE;
      if(io.UpKeyHeld){
         vertical += io.UP;
      }
      if(io.DownKeyHeld){
         vertical += io.DOWN;
      }

   }

   private void HandleAction(){
      if(currentPosseableObject !=null){
         currentPosseableObject.HandleAction();
      }
      else{
         
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
