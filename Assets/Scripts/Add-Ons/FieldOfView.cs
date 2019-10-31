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
   [SerializeField] [Range(0,360)] private float viewAngle;
   [SerializeField] [Range(0,360)] private float direction;
   [SerializeField] private int x_reflection = 1;
   [SerializeField] private LayerMask targetLayer;
   [SerializeField] private LayerMask obstacleLayer;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start() {
      visibleTargets = new List<Transform>();
      targetsInRange = new List<Transform>();
   }

   // ------------------------------------------------------
   // Target Detection
   // ------------------------------------------------------
   public void FindVisibleTargets() {
      visibleTargets.Clear();
      Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetLayer);

      foreach(Collider2D target in targetsInViewRadius){
         Vector2 dirToTarget = (target.transform.position - this.transform.position).normalized;

         if(Vector2.Angle(DirectionVector(), dirToTarget) < viewAngle/2){
            float distToTarget = Vector2.Distance(target.transform.position, this.transform.position);

            if(!Physics2D.Raycast(this.transform.position, dirToTarget, distToTarget, obstacleLayer)){
               visibleTargets.Add(target.transform);
            }
         }
      }
   }

   public void FindTargetsInRange(){
      targetsInRange.Clear();
      foreach(Transform target in visibleTargets){
         if(Vector2.Distance(target.position, this.transform.position) <= range){
            targetsInRange.Add(target);
         }
      }
   }

   public void FindTargets(){
      FindVisibleTargets();
      FindTargetsInRange();
   }

   public bool TargetInRange(Transform target){
      return targetsInRange.Contains(target);
   }

   // ------------------------------------------------------
   // Vectors
   // ------------------------------------------------------
   public Vector2 DirectionVector(){
      float angle = (direction - transform.eulerAngles.z) * Mathf.Deg2Rad;
      return new Vector2(viewRadius * Mathf.Sin(angle) * x_reflection, viewRadius * Mathf.Cos(angle));
   }
   public Vector2 VectorA(){
      float angle = (viewAngle + direction * 2 - transform.eulerAngles.z * 2) * Mathf.Deg2Rad / 2.0f;
      return new Vector2(viewRadius * Mathf.Sin(angle) * x_reflection, viewRadius * Mathf.Cos(angle));
   }
   public Vector2 VectorB(){
      float angle = (viewAngle - direction * 2 + transform.eulerAngles.z * 2) * Mathf.Deg2Rad / 2.0f;
      return new Vector2(-viewRadius * Mathf.Sin(angle) * x_reflection, viewRadius * Mathf.Cos(angle));
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void ReflectOverXAxis(bool yes) {
      x_reflection = (yes) ? -1 : 1;
   }

   public Transform ClosestTarget() {
      FindTargets();

      if(visibleTargets.Count <= 0) {
         return null;
      }
      else {
         Transform closestTarget = visibleTargets[0];
         float minDist = Vector2.Distance(closestTarget.position, this.transform.position);

         foreach(Transform target in visibleTargets){
            float dist = Vector2.Distance(target.position, this.transform.position);

            if(dist < minDist){
               minDist = dist;
               closestTarget = target;
            }
         }  
         return closestTarget;
      }
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public float Range {
      get{return range;}
   }

   public float ViewRadius {
      get{return viewRadius;}
   }

   public float ViewAngle {
      get{return viewAngle;}
   }
}
