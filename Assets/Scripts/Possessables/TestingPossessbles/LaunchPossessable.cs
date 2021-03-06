﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPossessable : Possessable
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Vector3 startPos;
   private bool dragFlag = false;
   private GameController control;

   [SerializeField] private float ClickRadius;
   [SerializeField] private float LaunchForce;
   [SerializeField] private float MaxLength;

   void Awake()
   {
      control = FindObjectOfType<GameController>();
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public override void OnFixedUpdate(InputController io)
   {
      // no movement
   }

   public override void OnUpdate(InputController io)
   {
      if (io.GetMouseDistanceFrom(this.transform) < ClickRadius && !dragFlag)
      {
         if (io.ActionKeyPressed)
         {
            dragFlag = true;
            startPos = io.MousePosition;
         }
      }
      else if (io.ActionKeyReleased && dragFlag)
      {
         dragFlag = false;
         LaunchCan(io.MousePosition);
         control.PossessionController.ForcedUnpossession(control.Player);
      }
   }

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void LaunchCan(Vector3 mousePos)
   {
      Vector2 launch = Vector2.ClampMagnitude(startPos - mousePos, MaxLength);
      rb.AddForceAtPosition(launch * LaunchForce, startPos);
   }
}
