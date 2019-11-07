using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessableDoor : Possessable
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool isClosed;
   private bool isLocked;
   private BoxCollider2D door;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void Start(){
      base.Start();

   }

   public override void OnUpdate(InputController io){
      if(io.ActionKeyPressed && !isLocked){
         ToggleDoorOpen();
      }
   }

   public override void OnFixedUpdate(InputController io){

   }
   

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void ToggleDoorOpen(){
      if(isClosed) {
         OpenDoor();
      } else { 
         CloseDoor();
      }
   }

   private void OpenDoor(){
      isClosed = false;
   }

   private void CloseDoor(){
      isClosed = true;
   }

   private void LockDoor(){
      CloseDoor();
      isLocked = true;
   }

   private void UnlockDoor(){
      isLocked = false;
   }
}
