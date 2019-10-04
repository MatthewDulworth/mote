using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All possesable objects should derive from this class
public class PossessableObject : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Rigidbody2D rb;
   private InputController io;
   private bool possessed;
   [SerializeField] private GameObject player;

   // ------------------------------------------------------
   // Mono
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();
      io = player.GetComponent<InputController>();
   }

   void Update(){
      OnUpdate();
   }

   // ------------------------------------------------------
   // Protected Methods
   // ------------------------------------------------------
   protected void OnUpdate(){
      // all update code goes here
      if(!possessed){
         CheckIfPlayerIsNear();
      }
   }

   protected void CheckIfPlayerIsNear(){
      // check if the player is within range of the object
   }

   // ------------------------------------------------------
   // Virtual Methods
   // ------------------------------------------------------
   public virtual void OnPossessedEnter(){
      // when the object gets possesed 
   }

   public virtual void OnPossessedExit(){
      // when the object stops being possesed
   }

   public virtual void HandleMovement(){
      
   }

   public virtual void HandleAction(){

   }
}
