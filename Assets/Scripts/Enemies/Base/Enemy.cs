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
   protected EnemyHealth enemyHealth;

   [SerializeField] protected float speed;
   [SerializeField] protected float damage;
   
   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start(){
      enemyHealth = GetComponentInChildren<EnemyHealth>();
      rb = GetComponent<Rigidbody2D>();
      fov = GetComponent<FieldOfView>();

      OnStart();
   }

   public abstract void OnFixedUpdate();
   public abstract void OnUpdate();
   public virtual void OnStart(){}
   public virtual void OnDeath(){}

   // ------------------------------------------------------
   // Targeting
   // ------------------------------------------------------
   public virtual void HandleTargeting(){
      currentTarget = fov.ClosestTarget();
   }
   
   public virtual bool TargetSighted(){
      return (currentTarget != null);
   }

   public virtual bool TargetInRange(){
      return fov.TargetInRange(currentTarget);
   }


   // ------------------------------------------------------
   // Movement
   // ------------------------------------------------------
   public virtual void ChangeVelocityRaw(float x, float y){
      rb.velocity = new Vector2(x ,y);
   }

   public virtual void MoveToPoint(Vector3 target){
      rb.transform.position = Vector2.MoveTowards(transform.position, target, speed*Time.deltaTime);
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public EnemyHealth Health{
      get{return enemyHealth;}
   }

   public abstract string GetCurrentStateName();
}
