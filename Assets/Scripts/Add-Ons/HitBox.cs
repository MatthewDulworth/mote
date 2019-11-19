using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HitBox : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private BoxCollider2D boxCollider;
   private List<GameObject> collidingObjects;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start()
   {
      boxCollider = GetComponent<BoxCollider2D>();
      collidingObjects = new List<GameObject>();

      if (!boxCollider.isTrigger)
      {
         boxCollider.isTrigger = true;
      }
   }

   // ------------------------------------------------------
   // Collisions
   // ------------------------------------------------------
   public void OnTriggerEnter2D(Collider2D collider)
   {
      collidingObjects.Add(collider.gameObject);
   }

   public void OnTriggerExit2D(Collider2D collider)
   {
      collidingObjects.Remove(collider.gameObject);
   }

   // ------------------------------------------------------
   // Checks
   // ------------------------------------------------------
   public bool IsColliding()
   {
      return collidingObjects.Count > 0;
   }

   public bool IsCollidingWith(string tag)
   {
      foreach (GameObject obj in collidingObjects)
      {
         if (obj.tag == tag)
         {
            return true;
         }
      }
      return false;
   }

   public bool IsCollidingWith(List<string> tags)
   {
      foreach (GameObject obj in collidingObjects)
      {
         foreach (string tag in tags)
         {
            if (obj.tag == tag)
            {
               return true;
            }
         }
      }
      return false;
   }

   public bool IsCollidingWith(GameObject obj)
   {
      return collidingObjects.Contains(obj);
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public List<GameObject> CollidingObjects
   {
      get { return collidingObjects; }
   }

   public BoxCollider2D Collider
   {
      get { return boxCollider; }
   }
}