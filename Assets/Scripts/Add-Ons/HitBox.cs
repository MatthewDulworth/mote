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
   private List<HitBox> collidingHitBoxes;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start()
   {
      boxCollider = GetComponent<BoxCollider2D>();
      collidingObjects = new List<GameObject>();
      collidingHitBoxes = new List<HitBox>();

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
      if (collider.gameObject.layer == this.gameObject.layer)
      {
         collidingHitBoxes.Add(collider.gameObject.GetComponent<HitBox>());
      }
   }

   public void OnTriggerExit2D(Collider2D collider)
   {
      collidingObjects.Remove(collider.gameObject);
      if (collider.gameObject.layer == this.gameObject.layer)
      {
         collidingHitBoxes.Remove(collider.gameObject.GetComponent<HitBox>());
      }
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

   public bool IsCollidingWithHitBox(string tag)
   {
      foreach (HitBox hitBox in collidingHitBoxes)
      {
         if (hitBox.tag == tag)
         {
            return true;
         }
      }
      return false;
   }

   public bool IsCollidingWithHitBox(List<string> tags)
   {
      foreach (HitBox hitBox in collidingHitBoxes)
      {
         foreach (string tag in tags)
         {
            if (hitBox.tag == tag)
            {
               return true;
            }
         }
      }
      return false;
   }

   public bool IsCollidingWithHitBox(HitBox hitBox)
   {
      return collidingHitBoxes.Contains(hitBox);
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public GameObject GetCollidingObject(string tag)
   {
      foreach (GameObject obj in collidingObjects)
      {
         if (obj.tag == tag)
         {
            return obj;
         }
      }
      return null;
   }

   public HitBox GetCollidingHitBox(string tag)
   {
      foreach (HitBox hitBox in collidingHitBoxes)
      {
         if (hitBox.tag == tag)
         {
            return hitBox;
         }
      }
      return null;
   }

   public List<GameObject> CollidingObjects
   {
      get { return collidingObjects; }
   }

   public BoxCollider2D Collider
   {
      get { return boxCollider; }
   }
}