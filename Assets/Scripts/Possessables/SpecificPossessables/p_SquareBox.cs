using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_SquareBox : Possessable
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] protected float movementSpeed;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void Start()
   {
      base.Start();
   }

   public override void OnUpdate(InputController io)
   {
      // nada
   }

   public override void OnFixedUpdate(InputController io)
   {
      int horizontal = io.GetHorizontalDirection();
      rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
   }
}
