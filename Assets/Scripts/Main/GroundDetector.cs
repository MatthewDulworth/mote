using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
   private bool onGround;

   [SerializeField] private LayerMask layerMask;
   [SerializeField] private float leftOffset;
   [SerializeField] private float rightOffset;

   private void DetectGround(){

      Vector3 leftOrigin = new Vector3(transform.position.x - leftOffset, transform.position.y, transform.position.z);
      Vector3 rightOrigin = new Vector3(transform.position.x + rightOffset, transform.position.y, transform.position.z);

      RaycastHit2D leftDetector = Physics2D.Raycast(leftOrigin, Vector2.down, 0.5f);
      RaycastHit2D rightDetector = Physics2D.Raycast(rightOrigin, Vector2.down, 0.5f);

      onGround = (leftDetector.collider != null || rightDetector.collider != null);
   }

   public bool OnGround{
      get{return onGround;}
   }
}
