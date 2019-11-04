using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Possessable : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected Rigidbody2D rb;
   protected SpriteRenderer sr;
   protected bool inRange;
   protected bool isPossessed;
   
   [SerializeField] protected float possesionRange;
   [SerializeField] protected float movementSpeed;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start() {
      rb = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
      inRange = false;
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public abstract void HandleMovement(InputController io);
   public abstract void HandleActions(InputController io);

   public virtual void OnEnterRange(){
      inRange = true;
      sr.color = new Color(1f,1f,1f,0.5f);
   }

   public virtual void OnExitRange(){
      inRange = false;
      sr.color = new Color(1f,1f,1f,1f);
   }

   public virtual void OnTargetEnter(){

   }

   public virtual void OnTargetExit(){
      
   }

   public virtual void OnPossessionEnter(){

   }

   public virtual void OnPossessionExit(){
      rb.velocity = Vector2.zero;
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public float PossesionRange {
      get{return possesionRange;}
   }

   public bool InRange {
      get{return inRange;}
   }
}
