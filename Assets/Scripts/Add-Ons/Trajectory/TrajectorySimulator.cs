using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrajectorySimulator : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private int resolution;           // the amount of line segemnts the arc should be drawn with
   [SerializeField] private float maxFallDistance;    //
   [SerializeField] private LayerMask collisionLayers;

   private LineRenderer lineRenderer;
   private int positionsCount;
   private float mass;
   private float gravity;


   // ------------------------------------------------------
   // Starts
   // ------------------------------------------------------
   public void OnValidate()
   {
      resolution = Mathf.Max(0, resolution);
      positionsCount = resolution + 1;
   }

   public void Awake()
   {
      lineRenderer = GetComponent<LineRenderer>();
      mass = GetComponent<Rigidbody2D>().mass;
      gravity = Physics2D.gravity.y;
   }

   // ------------------------------------------------------
   // Simulate Trajectory
   // ------------------------------------------------------
   public void SimulateTrjectory(Vector2 force)
   {
      ProjectileArc arc = new ProjectileArc(force, gravity, mass, maxFallDistance);
      Vector3[] points = new Vector3[positionsCount + 1];

      float timeStep = arc.TimeOfFlight / (float)positionsCount;

      for (int i = 0; i <= positionsCount; i++)
      {
         points[i] = arc.Position(i * timeStep) + (Vector2)transform.position;
      }

      Vector3[] positions = ComputeCollsions(points);
      RenderTrajectory(points);
   }

   // ------------------------------------------------------
   // Compute Collsions 
   // ------------------------------------------------------
   private Vector3[] ComputeCollsions(Vector3[] points)
   {
      int count = 0;
      for (int i = 0; i < positionsCount; i++)
      {
         Vector3 direction = points[i] - points[i + 1];
         float distance = Vector3.Distance(points[i], points[i + 1]);
         RaycastHit2D hit = Physics2D.Raycast(points[i], direction, distance, collisionLayers);

         if (hit.collider == null)
         {
            count++;
         }
      }

      Vector3[] positions = new Vector3[count];

      for(int i=0; i < count; i++)
      {
         positions[i] = points[i];
      }

      return positions;
   }

   // ------------------------------------------------------
   // Render Trajectory
   // ------------------------------------------------------
   private void RenderTrajectory(Vector3[] positions)
   {
      lineRenderer.positionCount = positionsCount + 1;
      lineRenderer.SetPositions(positions);
   }
}
