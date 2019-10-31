using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
   private bool onGround;
   private BoxCollider2D boxCollider2D;
   private Vector3 leftOrigin;
   private Vector3 rightOrigin;

   [SerializeField] private LayerMask detectionLayer;
   [SerializeField] private bool useObjectWidthAsOffset = true;
   [SerializeField] private float leftOffset;
   [SerializeField] private float rightOffset;
   [SerializeField] private float detectionRange = 0.5f;
   [SerializeField] private bool DebugMode = false;


   void Start(){
      SetCollider();
      FindOrigins();
   }

   public void SetCollider(){
      boxCollider2D = GetComponent<BoxCollider2D>();
   }

   public void DetectGround(){
      if(DebugMode){
         FindOrigins();
         Debug.Log(onGround);
      }

      RaycastHit2D leftDetector = Physics2D.Raycast(leftOrigin, Vector2.down, detectionRange, detectionLayer);
      RaycastHit2D rightDetector = Physics2D.Raycast(rightOrigin, Vector2.down, detectionRange, detectionLayer);


      onGround = (leftDetector.collider != null || rightDetector.collider != null);
   }

   public void FindOrigins(){

      if(useObjectWidthAsOffset){
         float offset = boxCollider2D.size.x;

         leftOffset = offset;
         rightOffset = offset;
      }
      leftOrigin = new Vector3(transform.position.x - leftOffset, transform.position.y, transform.position.z);
      rightOrigin = new Vector3(transform.position.x + rightOffset, transform.position.y, transform.position.z);
   }

   public bool OnGround{
      get{return onGround;}
   }

   public Vector3 LeftOrigin{
      get{return leftOrigin;}
   }

   public Vector3 RightOrigin{
      get{return rightOrigin;}
   }

   public float DetectionDistance{
      get{return detectionRange;}
   }
}