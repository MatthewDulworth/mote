using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EnemyCustomEditorMono))]
public class EnemyEditor : Editor
{
   void OnSceneGUI(){
      EnemyCustomEditorMono jankyVar = (EnemyCustomEditorMono)target;
      if(jankyVar.FindEnemy()){

         Enemy enemy = jankyVar.enemy;
         
         if(Application.isPlaying && jankyVar.DisplayState){
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 18;
   
            Handles.Label(enemy.gameObject.transform.position, enemy.GetCurrentStateName(), style);
         }
      }
   }
}
