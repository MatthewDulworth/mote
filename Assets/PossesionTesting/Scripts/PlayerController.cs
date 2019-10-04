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
   private PossessableObject currentPosObj;
   private List<PossessableObject> inRangeList;
   [SerializeField] private float movementSpeed;
   [SerializeField] private float diagonalLimiter;

   bool WTF;
   

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start() {
      inRangeList = new List<PossessableObject>();
      rb = GetComponent<Rigidbody2D>();
      io = GetComponent<InputController>();
      WTF = false;
   }

   void Update(){
      Debug.Log(WTF); // this is never true how in the absolute fuck
      HandleAction();
   }

   void FixedUpdate(){
      HandleMovement();
   }

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void HandleMovement(){
      // either use the players built in movement, or if possessing an object, the objects movement
      // Debug.Log(currentPosObj);
      if(currentPosObj != null){
         currentPosObj.HandleMovement();
      } 
      else {
         PlayerMovement();
      }
   }

   private void PlayerMovement(){  
      // this is the player regular movement 
      float horizontal = io.GetHorizontalDirection();
      float vertical = io.getVerticalDirection();

      if(horizontal != 0 && vertical !=0){
         horizontal *= diagonalLimiter;
         vertical *= diagonalLimiter;
      }

      rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
   }

   private void HandleAction(){
      // either handle the players actions, or the object it is possessings actions
      if(currentPosObj != null){
         currentPosObj.HandleAction();
         Debug.Log("current not null");
      }
      else{
         Debug.Log(inRangeList.Count); // even tho this says it is
         // BUG: this is never greater than zero, I dont know why
         if(inRangeList.Count > 0){
            HandlePossesions();
         }
      }
   }

   private void HandlePossesions(){
      Debug.Log("handling possesions");
      PossessableObject targetedPosObj = inRangeList[0];
      float minDistance = getDistance(inRangeList[0]);

      foreach(PossessableObject posObj in inRangeList) {

         float distance = getDistance(posObj);

         if(distance < minDistance){
            minDistance = distance;
            targetedPosObj = posObj;
         }
      }

      if(io.ActionKeyPressed){
         Possess(targetedPosObj);
      }
   }

   private float getDistance(PossessableObject posObj){
      float distance =  Mathf.Abs(transform.position.sqrMagnitude - posObj.transform.position.sqrMagnitude);
      Debug.Log(distance);
      return distance;
   }

   private void Possess(PossessableObject obj){
      if(currentPosObj != null){
         currentPosObj.OnPossessedExit();
      }
      currentPosObj = obj;
      currentPosObj.OnPossessedEnter();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void PosObjInRange(PossessableObject posObj){
      // this method is called by a possessable object whenever it comes in range
      inRangeList.Add(posObj);
      WTF = true;
      Debug.Log(inRangeList.Count); // BUG: should only print 1 in the test room, is alwaays bigger
   }

   public void PosObjOutOfRange(PossessableObject posObj){
      // call this method by a possessable object whenever it leaves range
      inRangeList.Remove(posObj);
      WTF = false;
      Debug.Log(inRangeList.Count); // BUG: should only print 1 in the test room, is alwaays bigger
   }
}
