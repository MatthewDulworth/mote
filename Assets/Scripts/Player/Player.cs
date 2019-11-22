using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Rigidbody2D rb;
   private HitBox hitBox;
   private bool hasControl = true;

   [SerializeField] private float range;
   [SerializeField] private float movementSpeed;
   [SerializeField] private float diagonalLimiter;


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void OnValidate()
   {
      range = Mathf.Max(range, 0);
      movementSpeed = Mathf.Max(movementSpeed, 0);
      diagonalLimiter = Mathf.Max(diagonalLimiter, 0);
   }

   void Start()
   {
      hitBox = gameObject.GetComponentInChildren<HitBox>();
      rb = GetComponent<Rigidbody2D>();
   }

   public void OnUpdate()
   {

   }

   // ------------------------------------------------------
   // Movement
   // ------------------------------------------------------
   public void HandleMovement(InputController io)
   {
      if (hasControl)
      {
         float horizontal = io.GetHorizontalDirection();
         float vertical = io.GetVerticalDirection();

         if (horizontal != 0 && vertical != 0)
         {
            horizontal *= diagonalLimiter;
            vertical *= diagonalLimiter;
         }
         rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
      }
   }

   public void StopMoving()
   {
      rb.velocity = Vector2.zero;
   }

   public void SetPosition(Vector3 pos)
   {
      transform.position = pos;
   }

   // ------------------------------------------------------
   // Impulse
   // ------------------------------------------------------
   public void AddImpulse(Vector2 impulse)
   {
      rb.AddForce(impulse, ForceMode2D.Impulse);
   }

   public IEnumerator AddImpulse(Vector2 impulse, float rateOfSlow)
   {
      RemoveControl();
      rb.AddForce(impulse, ForceMode2D.Impulse);

      while (rb.velocity != Vector2.zero)
      {
         if (hitBox.IsCollidingWith("Wall"))
         {
            StopMoving();
            GiveControl();
            yield break;
         }
         rb.AddForce(-impulse * rateOfSlow, ForceMode2D.Impulse);
         yield return null;
      }

      GiveControl();
   }

   // ------------------------------------------------------
   // Control
   // ------------------------------------------------------
   public void RemoveControl()
   {
      if (hasControl)
      {
         hasControl = false;
         rb.velocity = Vector2.zero;
      }
   }

   public void GiveControl()
   {
      if (!hasControl)
      {
         hasControl = true;
      }
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public Rigidbody2D RB
   {
      get { return rb; }
   }

   public float Range
   {
      get { return range; }
   }

   public HitBox HitBox
   {
      get { return hitBox; }
   }
}
