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
   protected HitBox hitbox;
   
   private LayerMask initialLayer;
   private string initialTag;
   
   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public virtual void Start() {
      initialLayer = gameObject.layer;
      initialTag = gameObject.tag;
      rb = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
      boxCollider2D = GetComponent<BoxCollider2D>();
      hitbox = GetComponentInChildren<HitBox>();
   }

   // ------------------------------------------------------
   // Updates
   // ------------------------------------------------------
   public abstract void OnFixedUpdate(InputController io);
   public abstract void OnUpdate(InputController io);

   // ------------------------------------------------------
   // Enter/Exit
   // ------------------------------------------------------
   public virtual void OnEnterRange(){
      sr.color = new Color(1f,1f,1f,0.5f);
   }

   public virtual void OnExitRange(){
      sr.color = new Color(1f,1f,1f,1f);
   }

   public virtual void OnTargetEnter(){
      transform.localScale += new Vector3(0.5f, 0.5f, 0);
   }

   public virtual void OnTargetExit(){
      transform.localScale -= new Vector3(0.5f, 0.5f, 0);
   }

   public virtual void OnPossessionEnter(){
      gameObject.layer = LayerMask.NameToLayer("Player");
      transform.GetChild(0).tag = "Player";
   }

   public virtual void OnPossessionExit(){
      gameObject.layer = initialLayer;
      rb.velocity = Vector2.zero;
      hitbox.tag = initialTag;
   }
}
