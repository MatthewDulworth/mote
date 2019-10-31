using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(WallAndEdgeDetector))]
public class WallAndEdgeDetectorEditor : Editor
{
   void OnSceneGUI(){
      WallAndEdgeDetector wed = (WallAndEdgeDetector)target;
      wed.FindOrigins();

      Handles.color = Color.yellow;
      
      Vector3 wallLineEnd = new Vector3(wed.WallDetectionRange, 0, 0);
      Vector3 edgeLineEnd = new Vector3(0, wed.EdgeDetectionRange, 0);

      Handles.DrawLine(wed.transform.position, wed.transform.position + wallLineEnd);
      Handles.DrawLine(wed.EdgeDetectorOrigin, wed.EdgeDetectorOrigin - edgeLineEnd);
   }
}
