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
   private bool reflection;
   
   [SerializeField] private Transform groundDetection;
   [SerializeField] private LayerMask wallLayer;
   [SerializeField] private int movementDirection = 1;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      rb = GetComponent<Rigidbody2D>();
      fov = GetComponent<FieldOfView>();
      reflection = false;

      if(movementDirection == -1){
         flip();
      }

      CurrentState = Patrol;
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public override void HandleAI(Transform target){
      CurrentState();
   }

   // ------------------------------------------------------
   // Patrol State
   // ------------------------------------------------------
   private void Patrol(){
      rb.velocity = new Vector2(movementDirection*speed, 0);

      RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, groundDetection.right, 0.001f, wallLayer);
      RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.5f, wallLayer);

      if(groundInfo.collider != null || wallInfo.collider == null){
         flip();
      } 

      fov.FindVisibleTargets();
      currentTarget = fov.ClosestTarget();

      if(TargetInRange() && OnGround()){
         ChangeState(Attack);
      }
      else if(TargetSighted()){
         ChangeState(Pursue);
      }
   }

   // ------------------------------------------------------
   // Pursue State
   // ------------------------------------------------------
   private void Pursue(){
      Debug.Log("Pursue");

      fov.FindVisibleTargets();
      currentTarget = fov.ClosestTarget();

      if(TargetInRange() && OnGround()){
         ChangeState(Attack);
      }
      else if(!TargetSighted()){
         ChangeState(Patrol);
      }
   }

   // ------------------------------------------------------
   // Attack State
   // ------------------------------------------------------
   private void Attack(){
      Debug.Log("Attack");

      fov.FindVisibleTargets();
      currentTarget = fov.ClosestTarget();

      if(TargetSighted() && OnGround()){
         ChangeState(Pursue);
      } 
      else if(!TargetSighted() && OnGround()) {
         ChangeState(Patrol);
      }
   }

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private bool TargetSighted(){
      return (currentTarget != null);
   }

   private bool TargetInRange(){
      return false;
   }

   private bool OnGround(){
      return true;
   }

   private void ChangeState(System.Action state){
      CurrentState = state;
      CurrentState();
   }

   private void flip(){
      movementDirection *= -1;

      Vector3 newScale = transform.localScale;
      newScale.x *= -1;
      transform.localScale = newScale;

      reflection = !reflection;
      fov.ReflectOverXAxis(reflection);
   }
}
