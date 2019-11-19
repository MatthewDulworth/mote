using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private List<Transform> visibleTargets;
   private List<Transform> targetsInRange;

   [SerializeField] private float range;
   [SerializeField] private float viewRadius;
   [SerializeField] [Range(0, 360)] private float viewAngle;
   [SerializeField] [Range(0, 360)] private float direction;
   [SerializeField] private int facingRight = 1;
   [SerializeField] private LayerMask targetLayer;
   [SerializeField] private LayerMask obstacleLayer;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start()
   {
      visibleTargets = new List<Transform>();
      targetsInRange = new List<Transform>();
   }

   void OnValidate()
   {
      range = Mathf.Max(range, 0);
      viewRadius = Mathf.Max(viewRadius, 0);

      range = Mathf.Min(range, viewRadius);
   }

   // ------------------------------------------------------
   // Target Detection
   // ------------------------------------------------------
   public void FindVisibleTargets()
   {
      visibleTargets.Clear();
      Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetLayer);

      foreach (Collider2D target in targetsInViewRadius)
      {
         Vector2 dirToTarget = (target.transform.position - this.transform.position).normalized;

         if (Vector2.Angle(DirectionVector(), dirToTarget) < viewAngle / 2)
         {
            float distToTarget = Vector2.Distance(target.transform.position, this.transform.position);

            if (!Physics2D.Raycast(this.transform.position, dirToTarget, distToTarget, obstacleLayer))
            {
               visibleTargets.Add(target.transform);
            }
         }
      }
   }

   public void FindTargetsInRange()
   {
      targetsInRange.Clear();

      foreach (Transform target in visibleTargets)
      {
         if (Vector2.Distance(target.position, this.transform.position) <= range)
         {
            targetsInRange.Add(target);
         }
      }
   }

   public void FindTargets()
   {
      FindVisibleTargets();
      FindTargetsInRange();
   }

   public bool TargetInRange(Transform target)
   {
      return targetsInRange.Contains(target);
   }

   // ------------------------------------------------------
   // Vectors
   // ------------------------------------------------------
   public Vector2 DirectionVector()
   {
      float angle = (direction - transform.eulerAngles.z) * Mathf.Deg2Rad;
      return new Vector2(viewRadius * Mathf.Sin(angle) * facingRight, viewRadius * Mathf.Cos(angle));
   }
   public Vector2 VectorA()
   {
      float angle = (viewAngle + direction * 2 - transform.eulerAngles.z * 2) * Mathf.Deg2Rad / 2.0f;
      return new Vector2(viewRadius * Mathf.Sin(angle) * facingRight, viewRadius * Mathf.Cos(angle));
   }
   public Vector2 VectorB()
   {
      float angle = (viewAngle - direction * 2 + transform.eulerAngles.z * 2) * Mathf.Deg2Rad / 2.0f;
      return new Vector2(-viewRadius * Mathf.Sin(angle) * facingRight, viewRadius * Mathf.Cos(angle));
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void ReflectOverXAxis(bool yes)
   {
      facingRight = (yes) ? -1 : 1;
   }

   public Transform ClosestTarget()
   {
      FindTargets();
      Transform[] targets = visibleTargets.ToArray();
      return Targeting.GetClosestTarget(targets, this.transform.position);
   }

   public int TargetOnLeftOrRight(Transform target)
   {
      Vector3 relativePoint = transform.InverseTransformPoint(target.position);

      if (relativePoint.x < 0.0)
      {
         // not entirely sure why i need to multiply it by -facingRight for it to work but i do
         return 1 * -facingRight;
      }
      else if (relativePoint.x > 0.0)
      {
         return -1 * -facingRight;
      }
      else
      {
         return 0;
      }
   }


   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public float Range
   {
      get { return range; }
   }

   public float ViewRadius
   {
      get { return viewRadius; }
   }

   public float ViewAngle
   {
      get { return viewAngle; }
   }
}
