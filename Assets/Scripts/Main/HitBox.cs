using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
   private new BoxCollider2D collider;

   void Start(){
      collider = GetComponent<BoxCollider2D>();
   }

   public BoxCollider2D Collider{
      get{return collider;}
   }
}
