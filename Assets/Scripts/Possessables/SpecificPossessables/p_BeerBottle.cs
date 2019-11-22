using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_BeerBottle : Possessable
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Vector3 startPos;
   private bool dragFlag = false;
   private GameController control;

   [SerializeField] private float clickRadius;
   [SerializeField] private float launchForce;
   [SerializeField] private float maxDragLength;
   [SerializeField] private float damageSpeed;

   // ------------------------------------------------------
   // Start
   // ------------------------------------------------------
   public void OnValidate()
   {
      clickRadius = Mathf.Max(0, clickRadius);
      launchForce = Mathf.Max(0, launchForce);
      maxDragLength = Mathf.Max(0, maxDragLength);
   }

   public override void Start()
   {
      base.Start();
      control = FindObjectOfType<GameController>();
   }

   // ------------------------------------------------------
   // Updates
   // ------------------------------------------------------
   public override void OnUpdate(InputController io)
   {
      HandleLaunches(io);
   }

   public override void OnNotPossessedUpdate(InputController io) { }

   public override void OnFixedUpdate(InputController io) { }

   // ------------------------------------------------------
   // Launch
   // ------------------------------------------------------
   private void HandleLaunches(InputController io)
   {
      if (io.GetMouseDistanceFrom(this.transform) < clickRadius && !dragFlag)
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
         Launch(io.MousePosition);
         control.PossessionController.ForcedUnpossession(control.Player);
      }
   }

   private void Launch(Vector3 mousePos)
   {
      Vector2 launch = Vector2.ClampMagnitude(startPos - mousePos, maxDragLength);
      rb.AddForceAtPosition(launch * launchForce, startPos);
   }

   // ------------------------------------------------------
   // Checks
   // ------------------------------------------------------
   public bool AtDamageSpeed()
   {
      return rb.velocity.magnitude >= damageSpeed;
   }
}
