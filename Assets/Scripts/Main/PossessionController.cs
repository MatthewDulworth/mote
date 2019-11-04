using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionController : MonoBehaviour
{
   [SerializeField] private LayerMask targetLayer;
   [SerializeField] private LayerMask obstacleLayer;

   private Possessable target;
   private Possessable possessedObject;
   private List<Possessable> visiblePossessables;

   void Start(){
      visiblePossessables = new List<Possessable>();
   }
   
   public void OnUpdate(Player player, InputController io){
      GetVisiblePossessables(player);
      GetTargetedPossessable(io);
   }

   public void GetVisiblePossessables(Player player){
      this.visiblePossessables.Clear();
      Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(player.transform.position, player.Range, targetLayer);

      foreach(Collider2D target in targetsInRange){

         Possessable pos = target.gameObject.GetComponent<Possessable>();
         if(pos != null){

            Vector2 direction = (target.transform.position - player.transform.position).normalized;
            float distance = Vector2.Distance(target.transform.position, player.transform.position);
           
            if(!Physics2D.Raycast(player.transform.position, direction, distance, obstacleLayer)){
               this.visiblePossessables.Add(pos);
            }
         }
      }
   }

   public void GetTargetedPossessable(InputController io){
      List<Transform> t = new List<Transform>();

      foreach(Possessable p in visiblePossessables){
         t.Add(p.transform);
      }
      Transform[] targets = t.ToArray();

      target = Targeting.GetClosestTarget(targets, io.MousePosition).GetComponent<Possessable>();
   }

   private void PossessObject(Possessable obj){

   }
}
