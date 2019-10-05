using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private PossessableObject currentPossessedObj;
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

      if(currentPossessedObj != null){
         // handle possessed actions
      }
      else{
         player.HandleActions(io);
      }

   }

   void FixedUpdate(){
      if(currentPossessedObj != null){
         // handle possessed movement
      }
      else{
         player.HandleMovement(io);
      }
   }

   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private void HandleRangeChecks(){
      foreach (Possessable obj in possessables) {
         if(obj != currentPossessedObj){
            if(PlayerInRangeOf(obj) && !obj.InRange){
               inRangeOfPlayerList.Add(obj);
               obj.OnEnterRange();
            } 
            else if(!PlayerInRangeOf(obj) && obj.InRange){
               inRangeOfPlayerList.Remove(obj);
               obj.OnExitRange();
            }
         }
      }
   }

   private bool PlayerInRangeOf(Possessable obj){
      return Physics2D.OverlapCircle(obj.transform.position, obj.PossesionRange, playerLayer);
   }

   private float GetDistance(Possessable obj){
      return Mathf.Abs(player.transform.position.sqrMagnitude - obj.transform.position.sqrMagnitude);
   }
}
