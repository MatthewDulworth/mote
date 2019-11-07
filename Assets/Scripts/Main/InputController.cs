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
   [SerializeField] private KeyCode UnpossessionKey;
   [SerializeField] private int PossessionKey;
   [SerializeField] private int ActionKey;

   private bool upKeyHeld;
   private bool downKeyHeld;
   private bool leftKeyHeld;
   private bool rightKeyHeld;
   private bool upKeyPressed;

   private bool actionKeyPressed;
   private bool actionKeyReleased;
   private bool possesionKeyPressed;
   private bool possesionKeyReleased;
   private bool unpossessionKeyPressed;

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
   void OnValidate(){
      if(ActionKey < 0) ActionKey = 0;
      if(ActionKey > 1) ActionKey = 1;

      if(PossessionKey < 0) PossessionKey = 0;
      if(PossessionKey > 1) PossessionKey = 1;

      if(ActionKey == 1 && PossessionKey == 1) ActionKey = 0;
      if(ActionKey == 0 && PossessionKey == 0) ActionKey = 1;
   }

   void Update() {
      GetMovementKeyValues();
      GetActionKeyValues();
      GetMouseKeyValues();
   }

   public void GetMovementKeyValues(){
      upKeyHeld = Input.GetKey(UpKey);
      downKeyHeld = Input.GetKey(DownKey);
      leftKeyHeld = Input.GetKey(LeftKey);
      rightKeyHeld = Input.GetKey(RightKey);

      upKeyPressed = Input.GetKeyDown(UpKey);
   }

   public void GetActionKeyValues(){
      actionKeyPressed = Input.GetMouseButtonDown(ActionKey);
      actionKeyReleased = Input.GetMouseButtonUp(ActionKey);
      possesionKeyPressed = Input.GetMouseButtonDown(PossessionKey);
      possesionKeyReleased = Input.GetMouseButtonUp(PossessionKey);
      unpossessionKeyPressed = Input.GetKeyDown(UnpossessionKey);
   }

   public void GetMouseKeyValues(){
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
   // Movement Keys
   // ------------------------------------------------------
   public bool UpKeyPressed{
      get{return upKeyPressed;}
   }

   // ------------------------------------------------------
   // Action Keys
   // ------------------------------------------------------
   public bool ActionKeyPressed{
      get{return actionKeyPressed;}
   }

   public bool ActionKeyReleased{
      get{return actionKeyReleased;}
   }

   public bool PossessionKeyPressed{
      get{return possesionKeyPressed;}
   }

   public bool PossessionKeyReleased{
      get{return possesionKeyReleased;}
   }

   public bool UnpossessionKeyPressed{
      get{return unpossessionKeyPressed;}
   }
   // ------------------------------------------------------
   // Mouse
   // ------------------------------------------------------
   public Vector3 MousePosition{
      get{return mousePosition;}
   }
}