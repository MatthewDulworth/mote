using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileArc
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   public float Gravity { get; private set; }         // the gravity being used
   public float Mass { get; private set; }            // the mass of the projectile
   public float InitialHeight { get; private set; }   // intitial height above the ground

   public float Velocity { get; private set; }        // velocity 
   public float VelocityX { get; private set; }       // x component of velocity 
   public float VelocityY { get; private set; }       // y componenet fo velocity 
   public float Angle { get; private set; }           // angle in radians relative to the forwards unit vector

   public float Range { get; private set; }           // the maximum x displacement of the projectile
   public float TimeOfFlight { get; private set; }    // time rquired for the projectile 
   public float MaxHeight { get; private set; }       // highest point the projectile reaches


   // ------------------------------------------------------
   // Constructor
   // ------------------------------------------------------
   public ProjectileArc(Vector2 force, float gravity, float mass, float height)
   { 
      Gravity = gravity;
      Mass = mass;
      InitialHeight = height;

      Velocity = force.magnitude * mass;
      VelocityX = force.x * mass;
      VelocityY = force.y * mass;
      Angle = Mathf.Atan2(force.y, force.x);
      
      Range = VelocityX * (VelocityY + Mathf.Sqrt(VelocityY * VelocityY + 2 * gravity * InitialHeight)) / gravity;
      TimeOfFlight = (VelocityY + Mathf.Sqrt(VelocityY * VelocityY + 2 * gravity * InitialHeight)) / gravity;
      MaxHeight = InitialHeight + VelocityY * VelocityY / (2 * gravity);
   }


   // ------------------------------------------------------
   // Position
   // ------------------------------------------------------
   public Vector2 Position(float time)
   {
      float x = VelocityX * time;
      float y = InitialHeight + VelocityY * time - Gravity * time * time / 2.0f;

      return new Vector2(x, y) * -1;
   }
}
