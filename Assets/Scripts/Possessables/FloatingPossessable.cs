using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPossessable : Possessable
{
   [SerializeField] private float diagonalLimiter;
   
   public override void HandleMovement(InputController io){
      float horizontal = io.GetHorizontalDirection();
      float vertical = io.GetVerticalDirection();

      if(horizontal != 0 && vertical !=0){
         horizontal *= diagonalLimiter;
         vertical *= diagonalLimiter;
      }

      rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
   }
}
