using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectorySimulator : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private int resolution;           // the amount of line segemnts the arc should be drawn with
   [SerializeField] private float maxFallDistance;    // 

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
      Vector3[] positions = new Vector3[positionsCount + 1];

      float timeStep = arc.TimeOfFlight / (float)positionsCount;

      for (int i = 0; i <= positionsCount; i++)
      {
         positions[i] = -arc.Position(i * timeStep) + (Vector2)transform.position;
      }

      RenderTrajectory(positions);
   }

   // ------------------------------------------------------
   // Render Trajectory
   // ------------------------------------------------------
   public void RenderTrajectory(Vector3[] positions)
   {
      lineRenderer.positionCount = positionsCount + 1;
      lineRenderer.SetPositions(positions);
   }

   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         Vector2 impulse = new Vector2(5, 10);
         Debug.LogFormat("Start: {0}", (Vector2)transform.position);
         SimulateTrjectory(impulse);
         this.GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);
      }
   }
}
