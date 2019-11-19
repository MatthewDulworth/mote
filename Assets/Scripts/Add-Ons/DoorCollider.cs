using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
   private new BoxCollider2D collider;

   void Start()
   {
      collider = GetComponent<BoxCollider2D>();
   }

   public void SetActive(bool active)
   {
      if (active)
      {
         collider.isTrigger = true;
      }
      else
      {
         collider.isTrigger = false;
      }
   }

   public BoxCollider2D Collider
   {
      get { return collider; }
   }
}
