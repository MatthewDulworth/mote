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
   private Transform eyes;
   private Animator animator;

   private bool hasControl = true;
   private float diagonalLimiter = 0.70f;

   private float zipRotationAngle = 0;
   private Vector3 eyeCenter;

   [SerializeField] private Animation curlAnimation;
   [SerializeField] private float range;
   [SerializeField] private float movementSpeed;
   [SerializeField] private float zipSpeed;
   [SerializeField] private float eyeDistance = 0.5f;
   [SerializeField] private float maxMoveSpeed;


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void OnValidate()
   {
      range = Mathf.Max(range, 0);
      movementSpeed = Mathf.Max(movementSpeed, 0);
      diagonalLimiter = Mathf.Max(diagonalLimiter, 0);
      eyeDistance = Mathf.Max(eyeDistance, 0);
   }

   void Start()
   {
      hitBox = gameObject.GetComponentInChildren<HitBox>();
      rb = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();

      eyes = transform.Find("Eyes");
      eyeCenter = eyes.localPosition;
   }

   public void OnUpdate()
   {
      TrackEyes();
   }


   // ------------------------------------------------------
   // Movement
   // ------------------------------------------------------
   public void HandleMovement(InputController io)
   {
      if (hasControl)
      {
         // movement 
         float horizontal = io.GetHorizontalDirection();
         float vertical = io.GetVerticalDirection();

         Vector2 direction = new Vector2(horizontal, vertical);
         rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxMoveSpeed);
         rb.AddForce(direction * movementSpeed);

         // animation 
         animator.SetBool("MovingRight", false);
         animator.SetBool("MovingLeft", false);

         if (horizontal > 0)
         {
            animator.SetBool("MovingRight", true);
         }
         else if (horizontal < 0)
         {
            animator.SetBool("MovingLeft", true);
         }
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
   // Eye Tracking
   // ------------------------------------------------------
   public void TrackEyes()
   {
      Vector3 mousePos = FindObjectOfType<InputController>().MousePosition;
      Vector3 mouseRelative = transform.InverseTransformPoint(mousePos);

      Vector3 dir = mouseRelative;
      dir = Vector3.ClampMagnitude(dir, eyeDistance);
      eyes.localPosition = dir + eyeCenter;
   }


   // ------------------------------------------------------
   // Zip
   // ------------------------------------------------------
   public void ZipTo(Vector3 target)
   {
      this.transform.position = Vector2.MoveTowards(this.transform.position, target, this.zipSpeed * Time.deltaTime);
   }

   public void RotateToPoint(Vector3 target)
   {
      Vector2 direction = this.transform.position - target;
      zipRotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

      this.rb.freezeRotation = false;
      this.transform.Rotate(0, 0, zipRotationAngle, Space.World);
      this.rb.freezeRotation = true;

      eyes.gameObject.SetActive(false);
      animator.SetBool("ZipFlag", true);
   }

   public void RotateBack()
   {
      this.rb.freezeRotation = false;
      this.transform.Rotate(0, 0, -zipRotationAngle, Space.World);
      this.rb.freezeRotation = true;

      eyes.gameObject.SetActive(true);
      animator.SetBool("ZipFlag", false);
   }

   public void PlayCurlAnimation()
   {
      // curlAnimation.Play();
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
   public bool HasControl
   {
      get { return hasControl; }
   }

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
