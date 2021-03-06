﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPossessable : Possessable
{
   [SerializeField] protected float movementSpeed;
   [SerializeField] private float JumpCheckRadius;
   [SerializeField] private float JumpForce;
   [SerializeField] private Transform GroundDetection;
   [SerializeField] private LayerMask WallLayer;

   public override void OnFixedUpdate(InputController io)
   {
      int horizontal = io.GetHorizontalDirection();
      rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

      if (IsGrounded() && io.UpKeyPressed)
      {
         rb.velocity = Vector2.up * JumpForce;
      }
   }

   public override void OnUpdate(InputController io)
   {

   }

   private bool IsGrounded()
   {
      return Physics2D.OverlapCircle(GroundDetection.position, JumpCheckRadius, WallLayer);
   }
}
