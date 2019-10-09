using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private FieldOfView fov;
   private Transform currentTarget;
   private System.Action CurrentState;
   
   [SerializeField] private Transform groundDetection;
   [SerializeField] private LayerMask wallLayer;
   [SerializeField] private int movementDirection = 1;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();
      fov = GetComponent<FieldOfView>();

      if(movementDirection == -1){
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
      if(this.currentTarget != target){
         this.currentTarget = target;
      }
      CurrentState();
   }

   // ------------------------------------------------------
   // States
   // ------------------------------------------------------
   private void Patrol(){
      rb.velocity = new Vector2(movementDirection*speed, 0);

      RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, groundDetection.right, 0.001f, wallLayer);
      RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.5f, wallLayer);

      if(groundInfo.collider != null || wallInfo.collider == null){
         // flip direction vector
         movementDirection *= -1;

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
