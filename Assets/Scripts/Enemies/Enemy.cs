using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Rigidbody2D rb;
   public float speed;
   public float sightRange;
   public float offsetFromPlayer;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public virtual void HandleMovement(Transform target){
      
   }
}
