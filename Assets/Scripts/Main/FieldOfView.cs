﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private List<Transform> visibleTargets;
   private int reflection;
   [SerializeField] private float viewRadius;
   [SerializeField] [Range(0,360)] private float viewAngle;
   [SerializeField] [Range(0,360)] private float direction;
   [SerializeField] private LayerMask targetLayer;
   [SerializeField] private LayerMask obstacleLayer;


   private int wait;
   private bool yeet;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      wait = 0;
      yeet = true;
      reflection = 1;
      visibleTargets = new List<Transform>();
   }

   // ------------------------------------------------------
   // Target Detection
   // ------------------------------------------------------
   public void FindVisibleTargets(){
      visibleTargets.Clear();
      Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetLayer);

      foreach(Collider2D target in targetsInViewRadius){
         Vector2 dirToTarget = (target.transform.position - this.transform.position).normalized;

         if(Vector2.Angle(DirectionVector(), dirToTarget) < viewAngle/2){
            float distToTarget = Vector2.Distance(target.transform.position, this.transform.position);

            if(!Physics2D.Raycast(this.transform.position, dirToTarget, distToTarget, obstacleLayer)){
               visibleTargets.Add(target.transform);
               Debug.Log(target.transform.gameObject);
            }
         }
      }
   }

   // ------------------------------------------------------
   // Vectors
   // ------------------------------------------------------
   public Vector2 DirectionVector(){
      float angle = (direction - transform.eulerAngles.z) * Mathf.Deg2Rad;
      return new Vector2(viewRadius * Mathf.Sin(angle) * reflection, viewRadius * Mathf.Cos(angle));
   }
   public Vector2 VectorA(){
      float angle = (viewAngle + direction * 2 - transform.eulerAngles.z * 2) * Mathf.Deg2Rad / 2.0f;
      return new Vector2(viewRadius * Mathf.Sin(angle) * reflection, viewRadius * Mathf.Cos(angle));
   }
   public Vector2 VectorB(){
      float angle = (viewAngle - direction * 2 + transform.eulerAngles.z * 2) * Mathf.Deg2Rad / 2.0f;
      return new Vector2(-viewRadius * Mathf.Sin(angle) * reflection, viewRadius * Mathf.Cos(angle));
   }

   public void ReflectOverXAxis(bool yes){
      reflection = (yes) ? -1 : 1;
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public float ViewRadius{
      get{return viewRadius;}
   }

   public float ViewAngle{
      get{return viewAngle;}
   }
}
