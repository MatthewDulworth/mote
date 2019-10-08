using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
   // ------------------------------------------------------
   // Private Vars
   // ------------------------------------------------------
   private System.Action CurrentState;
  
   // ------------------------------------------------------
   // Public Vars
   // ------------------------------------------------------
   public Transform GroundDetection;
   public LayerMask WallLayer;
   public int MovementDirection = 1;
   public float ViewAngle;


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();

      if(MovementDirection == -1){
         Vector3 newScale = transform.localScale;
         newScale.x *= -1;
         transform.localScale = newScale;
      }

      CurrentState = Patrol;
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public override void HandleAI(Transform target){
      
   }

   // ------------------------------------------------------
   // States
   // ------------------------------------------------------
   private void Patrol(){
      rb.velocity = new Vector2(MovementDirection*speed, 0);

      RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, GroundDetection.right, 0.001f, WallLayer);
      RaycastHit2D wallInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, 0.5f, WallLayer);

      if(groundInfo.collider != null || wallInfo.collider == null){
         // flip direction vector
         MovementDirection *= -1;

         // flip object
         Vector3 newScale = transform.localScale;
         newScale.x *= -1;
         transform.localScale = newScale;
      } 

      if(PlayerSighted()){
         ChangeState(Pursue);
      }
   }

   private void Pursue(){
      if(PlayerInRange() && Grounded()){
         ChangeState(Attack);
      }
   }

   private void Attack(){
      if(PlayerSighted()){
         ChangeState(Pursue);
      } else {
         ChangeState(Patrol);
      }
   }

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private bool PlayerSighted(){
      return false;
   }

   private bool PlayerInRange(){
      return false;
   }

   private bool Grounded(){
      return false;
   }

   private void ChangeState(System.Action state){
      CurrentState = state;
      CurrentState();
   }
}
