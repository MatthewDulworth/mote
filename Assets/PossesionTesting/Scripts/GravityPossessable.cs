using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPossessable : Possessable
{
   public float jumpCheckRadius;
   public Transform feetPos;
   public LayerMask wallLayer;

   public override void HandleMovement(InputController io){
      int horizontal = io.GetHorizontalDirection();
      rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
   }

   private bool IsGrounded(){
      return Physics2D.OverlapCircle(feetPos.position, jumpCheckRadius, wallLayer);
   }
}
