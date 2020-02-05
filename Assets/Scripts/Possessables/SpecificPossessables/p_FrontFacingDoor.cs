using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class p_FrontFacingDoor : Possessable
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private bool isLocked;

   private bool isClosed = true;
   private bool isBlocked = false;
   private Animator animator;


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void Start()
   {
      animator = GetComponent<Animator>();
      base.Start();
      if (!boxCollider2D.isTrigger)
      {
         boxCollider2D.isTrigger = true;
      }
   }

   public override void OnUpdate(InputController io)
   {
      isBlocked = hitBox.IsCollidingWith("Block");

      if (io.ActionKeyPressed)
      {
         ToggleOpen();
      }
   }

   public override void OnFixedUpdate(InputController io) { }

   public override void OnNotPossessedUpdate(InputController io){
      if(hitBox.IsCollidingWith("Player") && io.ActionKeyPressed && !isClosed){
         UseDoor();
      }
   }

   // ------------------------------------------------------
   // Exit
   // ------------------------------------------------------
   public void UseDoor(){
      Debug.Log("using door");
   }

   // ------------------------------------------------------
   // Open And Lock
   // ------------------------------------------------------
   public void ToggleOpen()
   {
      if (isClosed)
      {
         Open();
      }
      else
      {
         Close();
      }
   }

   public bool Open()
   {
      if (!isLocked && !isBlocked && isClosed)
      {
         isClosed = false;
         Debug.Log("open");
         GetComponent<Animator>().SetBool("open", true);
         return true;
      }
      else
      {
         GetComponent<Animator>().SetTrigger("locked");
         Debug.Log("Door is locked, blocked, or already open");
         return false;
      }
   }

   public bool Close()
   {
      if (!isClosed)
      {
         isClosed = true;
         Debug.Log("close");
         GetComponent<Animator>().SetBool("open", false);
         return true;
      }
      else
      {
         return false;
      }
   }

   public bool Lock()
   {
      if (!isLocked)
      {
         Close();
         isLocked = true;
         return true;
      }
      else
      {
         return false;
      }
   }

   public bool Unlock()
   {
      if (isLocked)
      {
         isLocked = false;
         return true;
      }
      else
      {
         return false;
      }
   }

   public bool IsClosed
   {
      get { return isClosed; }
   }
}
