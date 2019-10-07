using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPossessable : Possessable
{
   public float JumpCheckRadius;
   public float JumpForce;
   public Transform GroundDetection;
   public LayerMask WallLayer;

   public override void HandleMovement(InputController io){
      int horizontal = io.GetHorizontalDirection();
      rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

      if(IsGrounded() && io.UpKeyPressed){
         rb.velocity = Vector2.up * JumpForce;
      }
   }

   private bool IsGrounded(){
      return Physics2D.OverlapCircle(GroundDetection.position, JumpCheckRadius, WallLayer);
   }
}
