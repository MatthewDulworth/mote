using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private float viewRadius;
   [SerializeField] [Range(0,360)] private float viewAngle;
   [SerializeField] [Range(0,360)] private float direction;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
     
   }

   void Update(){
      
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public Vector2 GetDirectionVector(){
      float angle = (direction - transform.eulerAngles.z) * Mathf.Deg2Rad;
      return new Vector2(viewRadius * Mathf.Sin(angle), viewRadius * Mathf.Cos(angle));
   }

   public Vector2 GetVectorA(){
      float angle = (viewAngle + direction * 2 - transform.eulerAngles.z * 2) * Mathf.Deg2Rad / 2.0f;
      return new Vector2(viewRadius * Mathf.Sin(angle), viewRadius * Mathf.Cos(angle));
   }
   public Vector2 GetVectorB(){
      float angle = (viewAngle - direction * 2 + transform.eulerAngles.z * 2) * Mathf.Deg2Rad / 2.0f;
      return new Vector2(-viewRadius * Mathf.Sin(angle), viewRadius * Mathf.Cos(angle));
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
