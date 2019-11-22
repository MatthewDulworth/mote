using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(FieldOfView))]
public abstract class Enemy : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected Rigidbody2D rb;
   protected FieldOfView fov;
   protected Transform currentTarget;
   protected HitBox hitBox;

   [SerializeField] protected float speed;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public virtual void OnValidate()
   {
      speed = Mathf.Max(0, speed);
   }

   void Start()
   {
      rb = GetComponent<Rigidbody2D>();
      fov = GetComponent<FieldOfView>();
      hitBox = GetComponentInChildren<HitBox>();

      OnStart();
   }

   public abstract void OnFixedUpdate();
   public abstract void OnUpdate();
   public virtual void OnStart() { }
   public virtual void OnDeath() { }

   // ------------------------------------------------------
   // Targeting
   // ------------------------------------------------------
   public virtual void HandleTargeting()
   {
      currentTarget = fov.ClosestTarget();
   }

   public virtual bool TargetSighted()
   {
      return (currentTarget != null);
   }

   public virtual bool TargetInRange()
   {
      return fov.TargetInRange(currentTarget);
   }


   // ------------------------------------------------------
   // Movement
   // ------------------------------------------------------
   public void ChangeVelocity(float x, float y)
   {
      rb.velocity = new Vector2(x, y);
   }

   public void ChangeVelocity(Vector2 v)
   {
      rb.velocity = v;
   }

   public void StopMoving(){
      rb.velocity = Vector2.zero;
   }

   public virtual void MoveToPoint(Vector3 target)
   {
      rb.transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
   }

   public IEnumerator AddImpulseIE(Vector2 impulse, float rateOfSlow)
   {
      yield return null;

      rb.AddForce(impulse, ForceMode2D.Impulse);

      while (rb.velocity != Vector2.zero)
      {
         rb.AddForce(-impulse * rateOfSlow, ForceMode2D.Impulse);
         yield return null;
      }
   }

   public void AddImpulse(Vector2 impulse, float rateOfSlow)
   {
      StartCoroutine(AddImpulseIE(impulse, rateOfSlow));
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public Transform CurrentTarget
   {
      get { return currentTarget; }
   }

   public float Speed
   {
      get { return speed; }
   }

   public HitBox HitBox
   {
      get { return hitBox; }
   }

   public abstract string GetCurrentStateName();
}
