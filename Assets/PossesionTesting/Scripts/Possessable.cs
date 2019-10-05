using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Possessable : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected Rigidbody2D rb;
   protected SpriteRenderer sr;
   protected bool inRange;
   [SerializeField] protected float possesionRange;
   [SerializeField] private float movementSpeed;
   [SerializeField] private float diagonalLimiter;

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
   public virtual void OnEnterRange(){
      inRange = true;
      sr.color = new Color(1f,1f,1f,0.5f);
   }

   public virtual void OnExitRange(){
      inRange = false;
      sr.color = new Color(1f,1f,1f,1f);
   }

   public virtual void OnPossessionEnter(){
      
   }

   public virtual void OnPossessionExit(){
      rb.velocity = Vector2.zero;
   }

   public virtual void HandleMovement(InputController io){
      float horizontal = io.GetHorizontalDirection();
      float vertical = io.getVerticalDirection();

      if(horizontal != 0 && vertical !=0){
         horizontal *= diagonalLimiter;
         vertical *= diagonalLimiter;
      }

      rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
   }

   public virtual void HandleActions(InputController io){

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
