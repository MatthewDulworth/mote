using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected Rigidbody2D rb;
   [SerializeField] protected float speed;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public abstract void HandleAI(Transform target);
}
