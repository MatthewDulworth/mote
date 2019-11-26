using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Targeting
{
   public static Transform GetClosestTarget(Transform[] targets, Vector2 point)
   {
      Transform bestTarget = null;
      float closestDistanceSqr = Mathf.Infinity;

      foreach (Transform potentialTarget in targets)
      {
         Vector2 directionToTarget = (Vector2)potentialTarget.position - point;
         float dSqrToTarget = directionToTarget.sqrMagnitude;

         if (dSqrToTarget < closestDistanceSqr)
         {
            closestDistanceSqr = dSqrToTarget;
            bestTarget = potentialTarget;
         }
      }

      return bestTarget;
   }
}
