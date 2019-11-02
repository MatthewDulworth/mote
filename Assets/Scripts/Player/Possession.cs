using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private List<Possessable> visiblePossessables;
   [SerializeField] private float range;
   [SerializeField] private LayerMask targetLayer;


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void OnValidate(){
      range = Mathf.Max(range, 0);
   }


   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public void FindVisiblePossessables() {

      this.visiblePossessables.Clear();
      Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);

      foreach(Collider2D target in targetsInRange){

         if(target.gameObject.GetComponent<Possessable>()){
            Vector2 direction = (target.transform.position - this.transform.position).normalized;
            float distance = Vector2.Distance(target.transform.position, transform.position);

            if(!Physics2D.Raycast(transform.position, direction, distance, targetLayer)){
               this.visiblePossessables.Add(target.transform.gameObject.GetComponent<Possessable>());
            }
         }
      }
   }

   public Possessable GetTargetedPossesable(InputController io){

      if(visiblePossessables.Count > 0){
         Possessable target = visiblePossessables[0];
         float minDistance = Vector2.Distance(io.MousePosition, target.transform.position);

         foreach(Possessable possess in visiblePossessables){
            float distance = Vector2.Distance(io.MousePosition, target.transform.position);

            if(distance < minDistance){
               distance = minDistance;
               target = possess;
            }
         }
         return target;
      }
      else{
         return null;
      }
   }
}
