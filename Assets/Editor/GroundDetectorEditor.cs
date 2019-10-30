using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(GroundDetector))]
public class GroundDetectorEditor : Editor
{
   void OnSceneGUI(){
      GroundDetector gd = (GroundDetector)target;
      gd.SetCollider();
      gd.FindOrigins();

      Handles.color = Color.blue;
      Vector3 lineEnd = new Vector3(0, gd.DetectionDistance, 0);

      Handles.DrawLine(gd.LeftOrigin, gd.LeftOrigin - lineEnd);
      Handles.DrawLine(gd.RightOrigin, gd.RightOrigin - lineEnd);
   }
}
