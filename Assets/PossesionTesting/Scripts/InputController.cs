using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour 
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private KeyCode UpKey;
   [SerializeField] private KeyCode DownKey;
   [SerializeField] private KeyCode LeftKey;
   [SerializeField] private KeyCode RightKey;
   [SerializeField] private KeyCode ActionKey;

   private bool upKeyHeld;
   private bool downKeyHeld;
   private bool leftKeyHeld;
   private bool rightKeyHeld;
   private bool actionKeyPressed;
   private bool upKeyPressed;

   private const int right = 1;
   private const int left = -1;
   private const int up = 1;
   private const int down = -1;
   private const int none = 0;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Update() {
      upKeyHeld = Input.GetKey(UpKey);
      downKeyHeld = Input.GetKey(DownKey);
      leftKeyHeld = Input.GetKey(LeftKey);
      rightKeyHeld = Input.GetKey(RightKey);
      actionKeyPressed = Input.GetKeyDown(ActionKey);
      upKeyPressed = Input.GetKeyDown(UpKey);
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public int GetHorizontalDirection(){
      int horizontal = none;
      if(rightKeyHeld){
         horizontal += right;
      }
      if(leftKeyHeld){
         horizontal += left;
      }
      return horizontal;
   }

   public int getVerticalDirection(){
      int vertical = none;
      if(upKeyHeld){
         vertical += up;
      }
      if(downKeyHeld){
         vertical += down;
      }
      return vertical;
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public bool ActionKeyPressed{
      get{return actionKeyPressed;}
   }

   public bool UpKeyPressed{
      get{return upKeyPressed;}
   }
}
