using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryTest : MonoBehaviour
{
   
   private InputController io;
   private TrajectorySimulator trajSim;
   private Rigidbody2D rb;
   private bool dragFlag = false;

   public float forceModifier = 10;
   public float clickRadius = 1f;
   public float k = 22;

   public void Awake()
   {
      rb = GetComponent<Rigidbody2D>();
      trajSim = GetComponent<TrajectorySimulator>();
      io = FindObjectOfType<InputController>();
   }

   public void Update()
   {
      Vector3 startPos = Vector3.forward;

      if (io.GetMouseDistanceFrom(this.transform) < clickRadius && !dragFlag)
      {
         if (io.ActionKeyPressed)
         {
            dragFlag = true;
            startPos = io.MousePosition;
         }
      }
      else if(!io.ActionKeyReleased && dragFlag)
      {
         trajSim.SimulateTrjectory(CalculateLaunchVector(startPos, io.MousePosition));
      }
      else if (io.ActionKeyReleased && dragFlag)
      {
         dragFlag = false;
         Launch(CalculateLaunchVector(startPos, io.MousePosition));
      }
   }

   private Vector2 CalculateLaunchVector(Vector3 startPos, Vector3 endPos)
   {
      Vector2 direction = (Vector2)(startPos - endPos).normalized;
      float distance = Vector2.Distance(startPos, endPos);
      float v = distance * Mathf.Sqrt(k/rb.mass);

      return v * direction;
   }

   private void Launch(Vector2 launchImpulse)
   {
      rb.AddForce(launchImpulse, ForceMode2D.Impulse);
   }
}
