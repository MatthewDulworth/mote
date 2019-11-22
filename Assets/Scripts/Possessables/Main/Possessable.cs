using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Possessable : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected Rigidbody2D rb;
   protected SpriteRenderer sr;
   protected BoxCollider2D boxCollider2D;
   protected HitBox hitBox;

   private LayerMask initialLayer;
   private string initialHitBoxTag;

   [SerializeField] private GameObject targetedSprite;
   private GameObject target;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public virtual void Start()
   {
      rb = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
      boxCollider2D = GetComponent<BoxCollider2D>();
      hitBox = GetComponentInChildren<HitBox>();
      initialLayer = gameObject.layer;
      initialHitBoxTag = hitBox.gameObject.tag;
   }

   // ------------------------------------------------------
   // Updates
   // ------------------------------------------------------
   public abstract void OnFixedUpdate(InputController io);
   public abstract void OnUpdate(InputController io);
   public virtual void OnNotPossessedUpdate(InputController io) { }

   // ------------------------------------------------------
   // Enter/Exit
   // ------------------------------------------------------
   public virtual void OnEnterRange()
   {
      sr.color = new Color(1f, 1f, 1f, 0.5f);
   }

   public virtual void OnExitRange()
   {
      sr.color = new Color(1f, 1f, 1f, 1f);
   }

   public virtual void OnTargetEnter()
   {
      target = Instantiate(targetedSprite);
      target.transform.position = this.transform.position;
      target.transform.parent = this.transform;
   }

   public virtual void OnTargetExit()
   {
      Destroy(target);
   }

   public virtual void OnPossessionEnter()
   {
      gameObject.layer = LayerMask.NameToLayer("Player");
      transform.GetChild(0).tag = "Player";
   }

   public virtual void OnPossessionExit()
   {
      gameObject.layer = initialLayer;
      rb.velocity = Vector2.zero;
      hitBox.tag = initialHitBoxTag;
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public HitBox HitBox
   {
      get { return hitBox; }
   }
}
