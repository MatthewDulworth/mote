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
   [SerializeField] private Sprite openSprite;
   [SerializeField] private Sprite closedSprite;
   [SerializeField] private bool isLocked;
   private bool isClosed = true;
   private bool isBlocked;


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void Start()
   {
      base.Start();
      if (!boxCollider2D.isTrigger)
      {
         boxCollider2D.isTrigger = true;
      }
   }

   public override void OnUpdate(InputController io)
   {
      if (io.ActionKeyPressed)
      {
         ToggleOpen();
      }
   }

   public override void OnFixedUpdate(InputController io) { }

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
      if (!isLocked && isClosed)
      {
         isClosed = false;
         sr.sprite = openSprite;
         return true;
      }
      else
      {
         return false;
      }
   }

   public bool Close()
   {
      if (!isClosed)
      {
         isClosed = true;
         sr.sprite = closedSprite;
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
}
