using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected Rigidbody2D rb;
   [SerializeField] protected float speed;
   [SerializeField] protected float offsetFromPlayer;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public virtual void HandleAI(Transform target){
      
   }
}
