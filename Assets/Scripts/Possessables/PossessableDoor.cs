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
   private DoorCollider doorCollider;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void Start(){
      base.Start();
      doorCollider = GetComponentInChildren<DoorCollider>();
   }

   public override void OnUpdate(InputController io){
      if(io.ActionKeyPressed && !isLocked){
         ToggleDoorOpen();
      }
   }

   public override void OnFixedUpdate(InputController io){
      // nada
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
      doorCollider.SetActive(false);
   }

   private void CloseDoor(){
      isClosed = true;
      doorCollider.SetActive(true);
   }

   private void LockDoor(){
      CloseDoor();
      isLocked = true;
   }

   private void UnlockDoor(){
      isLocked = false;
   }
}
