using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Player))]
public class PlayerEditor : Editor
{
   void OnSceneGUI(){
      Player player = (Player)target;

      Handles.color = Color.white;
      Handles.DrawWireDisc(player.transform.position, Vector3.back, player.Range);
   }
}
