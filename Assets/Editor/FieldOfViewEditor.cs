using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(FieldOfView))]
public class FieldOfViewEditor : Editor {
   void OnSceneGUI(){
      FieldOfView fov = (FieldOfView)target;

      Handles.color = Color.white;
      Handles.DrawWireDisc(fov.transform.position, Vector3.back, fov.ViewRadius);

      Handles.DrawLine(fov.transform.position, fov.transform.position + (Vector3)fov.GetVectorA());
      Handles.DrawLine(fov.transform.position, fov.transform.position + (Vector3)fov.GetVectorB());
      
      Handles.color = Color.red;
      Handles.DrawLine(fov.transform.position, fov.transform.position + (Vector3)fov.GetDirectionVector()); 
   }
}
