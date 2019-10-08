using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPossessable : Possessable
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Vector3 startPos;
   private bool dragFlag = false;
   private GameController control;

   [SerializeField] private float ClickRadius;
   [SerializeField] private float LaunchForce;
   [SerializeField] private float MaxLength;
   
   void Awake(){
      control = FindObjectOfType<GameController>();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public override void HandleMovement(InputController io){
      // no movement
   }

   public override void HandleActions(InputController io){
      if(io.GetMouseDistanceFrom(this.transform) < ClickRadius && !dragFlag){
         if(io.LeftMouseButtonDown){
            dragFlag = true;
            startPos = io.MousePosition;
         }
      }
      else if(io.LeftMouseButtonUp && dragFlag){
         dragFlag = false;
         LaunchCan(io.MousePosition);
         control.UnpossessObject();
      }
   }

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void LaunchCan(Vector3 mousePos){
      Vector2 launch = Vector2.ClampMagnitude(startPos - mousePos, MaxLength);
      rb.AddForceAtPosition(launch * LaunchForce, startPos);
   }
}
