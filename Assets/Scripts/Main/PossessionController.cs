using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private LayerMask targetLayer;
   [SerializeField] private LayerMask obstacleLayer;

   private PossessableContainer target;
   private PossessableContainer possessedContainer;
   private List<PossessableContainer> visiblePossessables;
   private List<PossessableContainer> possessables;


   // ------------------------------------------------------
   // Struct
   // ------------------------------------------------------
   private class PossessableContainer
   {
      public Possessable Possessable {get; set;}
      public bool InRange {get; set;}
      public bool IsPossessed {get; set;}

      public PossessableContainer(Possessable p){
         this.Possessable = p;
         this.InRange = false;
         this.IsPossessed = false;
      }
   }


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public void OnStart(List<Possessable> pos){
      visiblePossessables = new List<PossessableContainer>();
      possessables = new List<PossessableContainer>();

      foreach(Possessable p in pos){
         possessables.Add(new PossessableContainer(p));
      }
   }
   
   public void OnUpdate(Player player, InputController io){
      GetVisiblePossessables(player);
      GetTargetedPossessable(io);
   }

   public void OnFixedUpdate(){

   }


   // ------------------------------------------------------
   // On Updates
   // ------------------------------------------------------
   private void GetVisiblePossessables(Player player){
      this.visiblePossessables.Clear();
      Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(player.transform.position, player.Range, targetLayer);

      foreach(Collider2D targetCollider in targetsInRange){

         Possessable pos = targetCollider.gameObject.GetComponent<Possessable>();
         if(pos != null){

            Vector2 direction = (targetCollider.transform.position - player.transform.position).normalized;
            float distance = Vector2.Distance(targetCollider.transform.position, player.transform.position);
           
            if(!Physics2D.Raycast(player.transform.position, direction, distance, obstacleLayer)){
               this.visiblePossessables.Add(GetPossessableContainer(pos));
            }
         }
      }
   }

   private void GetTargetedPossessable(InputController io){

      List<Transform> t = new List<Transform>();
      foreach(PossessableContainer p in visiblePossessables){
         t.Add(p.Possessable.transform);
      }
      Transform[] targets = t.ToArray();

      Possessable possess = Targeting.GetClosestTarget(targets, io.MousePosition).GetComponent<Possessable>();
      target = (possess != null) ? GetPossessableContainer(possess) : null;
   }

   private void HandleTargetingGraphics(){
      
   }


   // ------------------------------------------------------
   // Possession
   // ------------------------------------------------------
   private void PossessObject(PossessableContainer posContainer){
      if(possessedContainer == null){
         possessedContainer = posContainer;
         possessedContainer.Possessable.OnPossessionEnter();
      }
      else{
         Debug.LogError("Cannot possess, already possessing an object");
      }
   }

   private void Unpossess(){
      if(possessedContainer != null){
         possessedContainer.Possessable.OnPossessionExit();
      }
      possessedContainer = null;
   }

   private void HandlePossessions(){

   }

   private void HandleUnpossessions(){

   }

   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private PossessableContainer GetPossessableContainer(Possessable pos){
      PossessableContainer posCon = null;

      foreach(PossessableContainer p in possessables){
         if(pos == p.Possessable){
            posCon = p;
         }
      }

      return posCon;
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public Possessable PossessedObject{
      get{return possessedContainer.Possessable;}
   }
}
