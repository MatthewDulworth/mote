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
      Debug.Log("entered range");
   }

   public virtual void OnExitRange(){
      inRange = false;
      sr.color = new Color(1f,1f,1f,1f);
      Debug.Log("exited range");
   }

   public virtual void OnPossessionEnter(){
      Debug.Log("yeet");
   }

   public virtual void OnPossessionExit(){
      Debug.Log("feet");
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
