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

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void Follow(Transform target){
      Vector3.MoveTowards(transform.position, target.position, speed);
   }
}
