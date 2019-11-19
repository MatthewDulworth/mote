using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HitBox : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private new BoxCollider2D collider;
   private List<string> tags;
   private List<GameObject> objectsColliding;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start()
   {
      collider = GetComponent<BoxCollider2D>();
      objectsColliding = new List<GameObject>();

      if (tags == null)
      {
         tags = new List<string>();
      }

      if (!collider.isTrigger)
      {
         collider.isTrigger = true;
      }
   }

   // ------------------------------------------------------
   // Collisions
   // ------------------------------------------------------
   public void OnTriggerEnter2D(Collider2D collider)
   {
      foreach (string tag in tags)
      {
         if (tag == collider.tag)
         {
            objectsColliding.Add(collider.gameObject);
         }
      }
   }

   public void OnTriggerExit2D(Collider2D collider)
   {
      objectsColliding.Remove(collider.gameObject);
   }

   // ------------------------------------------------------
   // Getters/Setters
   // ------------------------------------------------------
   public void SetCollisionTags(List<string> tags)
   {
      this.tags = tags;
   }

   public bool IsColliding()
   {
      return (objectsColliding.Count > 0);
   }

   public List<GameObject> ObjectsColliding
   {
      get { return objectsColliding; }
   }

   public BoxCollider2D Collider
   {
      get { return collider; }
   }
}