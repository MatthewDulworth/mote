using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoor : Possessable
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool isOpen = false;
   private BoxCollider2D collidesWithPlayer;
   private GameController control;

   // ------------------------------------------------------
   // Methods
   // ------------------------------------------------------
   public override void Start(){
      base.Start();
      control = FindObjectOfType<GameController>(); 
      collidesWithPlayer = GetComponentInChildren<BoxCollider2D>();
   }

   public override void OnUpdate(InputController io){
      if(io.ActionKeyPressed && !isOpen){
         isOpen = true;
         Destroy(transform.GetChild(0).gameObject);
         control.ForceUnpossession();
      }
   }
   public override void OnFixedUpdate(InputController io){
      
   }
}
