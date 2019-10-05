using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Possessable possessedObj;
   private List<Possessable> inRangeOfPlayerList;
   private List<Possessable> possessables;

   [SerializeField] private Player player;
   [SerializeField] private InputController io;
   [SerializeField] private LayerMask playerLayer;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start() {
      inRangeOfPlayerList = new List<Possessable>();
      possessables = new List<Possessable>();
      Possessable[] pos = FindObjectsOfType<Possessable>();

      foreach(Possessable obj in pos){
         possessables.Add(obj);
      }
   }

   void Update() {
      HandleRangeChecks();

      if(possessedObj != null){
         possessedObj.HandleActions(io);
         HandleUnpossessions();
      }
      else{
         HandlePossessions();
      }
   }

   void FixedUpdate(){
      if(possessedObj != null){
         possessedObj.HandleMovement(io);
      }
      else{
         player.HandleMovement(io);
      }
   }
   

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void HandleRangeChecks(){
      if(possessedObj == null){
         foreach (Possessable obj in possessables) {
            if(PlayerInRangeOf(obj) && !obj.InRange){
               inRangeOfPlayerList.Add(obj);
               obj.OnEnterRange();
               Debug.Log(inRangeOfPlayerList.Count);
            } 
            else if(!PlayerInRangeOf(obj) && obj.InRange){
               inRangeOfPlayerList.Remove(obj);
               obj.OnExitRange();
               Debug.Log(inRangeOfPlayerList.Count);
            }
         }
      }
   }

   private bool PlayerInRangeOf(Possessable obj){
      return Physics2D.OverlapCircle(obj.transform.position, obj.PossesionRange, playerLayer);
   }

   private Possessable GetTargetedPossessable(){
      if(inRangeOfPlayerList.Count > 0){

         Possessable target = inRangeOfPlayerList[0];
         float minDistance = GetDistance(target);

         foreach(Possessable obj in inRangeOfPlayerList){
            float distance = GetDistance(obj);
            if(distance < minDistance){
               target = obj;
               minDistance = distance;
            }
         }
         return target;
      } 
      else {
         return null;
      }
   }

   private float GetDistance(Possessable obj){
      return Mathf.Abs(player.transform.position.sqrMagnitude - obj.transform.position.sqrMagnitude);
   }

   private void HandlePossessions(){
      Possessable target = GetTargetedPossessable();
      if(target != null){
         if(io.ActionKeyPressed){

            foreach(Possessable obj in inRangeOfPlayerList){
               obj.OnExitRange();
            }

            PossessObject(target);
         }
      }
   }

   private void PossessObject(Possessable obj){
      player.StopMoving();
      inRangeOfPlayerList.Clear();
      if(possessedObj != null){
         possessedObj.OnPossessionExit();
      }
      possessedObj = obj;
      possessedObj.OnPossessionEnter();
   }

   private void HandleUnpossessions(){
      if(io.ActionKeyPressed){
         UnpossessObject();
      }
   }

   private void UnpossessObject(){
      possessedObj.OnPossessionExit();
      possessedObj = null;
   }
}
