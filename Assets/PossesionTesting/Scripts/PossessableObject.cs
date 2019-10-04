using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All possesable objects should derive from this class
public class PossessableObject : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Rigidbody2D rb;
   private InputController io;
   private PlayerController player;
   private SpriteRenderer sr;
   private bool possessed;
   private bool inRange;

   [SerializeField] private GameObject playerObject;
   [SerializeField] private float possesionRange;
   [SerializeField] private LayerMask playerLayer;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();
      io = playerObject.GetComponent<InputController>();
      player = playerObject.GetComponent<PlayerController>();
      sr = GetComponent<SpriteRenderer>();
      inRange = false;
      possessed = false;
   }

   void Update(){
      OnUpdate();
   }

   // ------------------------------------------------------
   // Protected Methods
   // ------------------------------------------------------
   protected void OnUpdate(){
      // all update code goes here
      if(!possessed){
         // if the player is in range, and the in range flag is not set
         if(PlayerIsInRange() && !inRange){
            player.PosObjInRange(this);
            inRange = true;
            sr.color = new Color(1f,1f,1f,0.5f);
         } 
         // else if the  player is not in range, and the in range flag is set
         else if(!PlayerIsInRange() && inRange) {
            player.PosObjOutOfRange(this);
            inRange = false;
            sr.color = new Color(1f,1f,1f,1f);
         }
      }
   }

   protected bool PlayerIsInRange(){
      // check if the player is within range of the object
      return Physics2D.OverlapCircle(transform.position, possesionRange, playerLayer);
   }

   // ------------------------------------------------------
   // Virtual Methods
   // ------------------------------------------------------
   public virtual void OnPossessedEnter(){
      // when the object gets possesed 
      Debug.Log("Possessed");
      possessed = true;
   }

   public virtual void OnPossessedExit(){
      // when the object stops being possesed
      Debug.Log("No longer possessed");
      possessed = false;
   }

   public virtual void HandleMovement(){
      Debug.Log("now using possessed movement");
   }

   public virtual void HandleAction(){
      Debug.Log("now using possessed actions");
   }
}
