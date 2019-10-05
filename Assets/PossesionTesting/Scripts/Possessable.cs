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
   [SerializeField] protected GameController controller;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Awake(){
      
   }

   void Start() {
      rb = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
      inRange = false;
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void OnEnterRange(){
      inRange = true;
      sr.color = new Color(1f,1f,1f,0.5f);
      Debug.Log("entered range");
   }

   public void OnExitRange(){
      inRange = false;
      sr.color = new Color(1f,1f,1f,1f);
      Debug.Log("exited range");
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
