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

   private bool leftMouseButtonDown;
   private bool leftMouseButtonUp;
   private bool leftMouseButtonHeld;

   private bool rightMouseButtonDown;
   private bool rightMouseButtonUp;
   private bool rightMouseButtonHeld;
   private Vector3 mousePosition;

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

      leftMouseButtonDown = Input.GetMouseButtonDown(0);
      leftMouseButtonUp = Input.GetMouseButtonUp(0);
      leftMouseButtonHeld = Input.GetMouseButton(0);

      rightMouseButtonDown = Input.GetMouseButtonUp(1);
      rightMouseButtonUp = Input.GetMouseButtonDown(1);
      rightMouseButtonHeld = Input.GetMouseButton(1);

      mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      mousePosition.z = 0;
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

   public int GetVerticalDirection(){
      int vertical = none;
      if(upKeyHeld){
         vertical += up;
      }
      if(downKeyHeld){
         vertical += down;
      }
      return vertical;
   }

   public float GetMouseDistanceFrom(Transform point){
      return Vector2.Distance(point.position, mousePosition);
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

   public bool LeftMouseButtonHeld{
      get{return leftMouseButtonHeld;}
   }

   public bool LeftMouseButtonUp{
      get{return leftMouseButtonUp;}
   }

   public bool LeftMouseButtonDown{
      get{return leftMouseButtonDown;}
   }

   public bool RightMouseButtonHeld{
      get{return rightMouseButtonHeld;}
   }

   public bool RightMouseButtonUp{
      get{return rightMouseButtonUp;}
   }

   public bool RightMouseButtonDown{
      get{return rightMouseButtonDown;}
   }

   public Vector3 MousePosition{
      get{return mousePosition;}
   }
}