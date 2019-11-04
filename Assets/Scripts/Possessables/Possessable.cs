using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Possessable : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   protected Rigidbody2D rb;
   protected SpriteRenderer sr;
   
   [SerializeField] protected float movementSpeed;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start() {
      rb = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
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
      Debug.LogFormat("Possessed {0}", this.gameObject);
   }

   public virtual void OnPossessionExit(){
      Debug.LogFormat("Unpoossessed {0}", this.gameObject);
      rb.velocity = Vector2.zero;
   }
}
