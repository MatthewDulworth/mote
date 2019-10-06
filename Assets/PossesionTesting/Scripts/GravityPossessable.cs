using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPossessable : Possessable
{
   public float jumpCheckRadius;
   public float jumpForce;
   public Transform feetPos;
   public LayerMask wallLayer;

   public override void HandleMovement(InputController io){
      int horizontal = io.GetHorizontalDirection();
      rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

      if(IsGrounded() && io.UpKeyPressed){
         rb.velocity = Vector2.up * jumpForce;
      }
   }

   private bool IsGrounded(){
      return Physics2D.OverlapCircle(feetPos.position, jumpCheckRadius, wallLayer);
   }
}
