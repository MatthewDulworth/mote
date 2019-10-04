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

   private const int right = 1;
   private const int left = -1;
   private const int up = 1;
   private const int down = -1;
   private const int none = 0;

   // ------------------------------------------------------
   // Mono
   // ------------------------------------------------------
   void Update() {
      upKeyHeld = Input.GetKey(UpKey);
      downKeyHeld = Input.GetKey(DownKey);
      leftKeyHeld = Input.GetKey(LeftKey);
      rightKeyHeld = Input.GetKey(RightKey);
      actionKeyPressed = Input.GetKeyDown(ActionKey);
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public bool UpKeyHeld{
      get {return upKeyHeld;}
   }
   public bool DownKeyHeld{
      get {return downKeyHeld;}
   }
   public bool LeftKeyHeld{
      get {return leftKeyHeld;}
   }
   public bool RightKeyHeld{
      get {return rightKeyHeld;}
   }
   public bool ActionKeyPressed{
      get {return actionKeyPressed;}
   }
   public int UP{
      get {return up;}
   }
   public int DOWN{
      get {return down;}
   }
   public int LEFT{
      get {return left;}
   }
   public int RIGHT{
      get {return right;}
   }
   public int NONE{
      get {return none;}
   }
}
