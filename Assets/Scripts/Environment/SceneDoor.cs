using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoor : Possessable
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool isOpen = false;
   private BoxCollider2D collid;

   // ------------------------------------------------------
   // Methods
   // ------------------------------------------------------
   public override void Start(){
      base.Start();
      collid = GetComponentInChildren<BoxCollider2D>();
   }

   public override void OnUpdate(InputController io){
      if(io.ActionKeyPressed){
         isOpen = true;
         collid.gameObject.SetActive(false);
      }
   }
   public override void OnFixedUpdate(InputController io){
      
   }
}
