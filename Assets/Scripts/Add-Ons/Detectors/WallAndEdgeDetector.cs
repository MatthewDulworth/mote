using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAndEdgeDetector : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool wallDetected;
   private bool edgeDetected;
   private Vector3 edgeDetectorOrigin;

   [SerializeField] private LayerMask detectionLayer;
   [SerializeField] private float wallDetectionRange = 1;
   [SerializeField] private float edgeDetectionRange = 1;
   [SerializeField] private float edgeDetectionOffset = 1;
   [SerializeField] private int x_reflection = 1;
   [SerializeField] private bool debugMode = false;

   // ------------------------------------------------------
   // Init Methods
   // ------------------------------------------------------
   public void FindOrigins()
   {
      edgeDetectorOrigin = new Vector3(transform.position.x + edgeDetectionOffset * x_reflection, transform.position.y, transform.position.z);
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void DetectWalls()
   {
      if (debugMode)
      {
         Debug.LogFormat("Wall Detected: {0}", wallDetected);
      }

      RaycastHit2D wallDetector = Physics2D.Raycast(transform.position, transform.right, wallDetectionRange * x_reflection, detectionLayer);
      wallDetected = (wallDetector.collider != null);
   }

   public void DetectEdges()
   {
      if (debugMode)
      {
         Debug.LogFormat("Edge Detected: {0}", edgeDetected);
      }

      FindOrigins();

      RaycastHit2D edgeDetector = Physics2D.Raycast(edgeDetectorOrigin, Vector2.down, wallDetectionRange, detectionLayer);
      edgeDetected = (edgeDetector.collider == null);
   }

   public void ReflectOverXAxis(bool yes)
   {
      x_reflection = (yes) ? -1 : 1;
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public float WallDetectionRange
   {
      get { return wallDetectionRange * x_reflection; }
   }

   public float EdgeDetectionRange
   {
      get { return edgeDetectionRange; }
   }

   public Vector3 EdgeDetectorOrigin
   {
      get { return edgeDetectorOrigin; }
   }

   public bool EdgeDetected
   {
      get { return edgeDetected; }
   }

   public bool WallDetected
   {
      get { return wallDetected; }
   }
}
