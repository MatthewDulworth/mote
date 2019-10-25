using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected Rigidbody2D rb;
   protected FieldOfView fov;

   [SerializeField] protected float speed;
   
   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();
      fov = GetComponent<FieldOfView>();

      OnStart();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public abstract void OnFixedUpdate();
   public abstract void OnUpdate();
   public virtual void OnStart(){}
}
