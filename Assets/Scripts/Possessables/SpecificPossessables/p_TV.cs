using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_TV : Possessable
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private Sprite onSprite;
   [SerializeField] private Sprite offSprite;
   private bool powerOn = false;

   // ------------------------------------------------------
   // Updates
   // ------------------------------------------------------
   public override void Start()
   {
      base.Start();
      PowerOff();
   }

   public override void OnUpdate(InputController io)
   {
      if (io.ActionKeyPressed)
      {
         TogglePower();
      }
   }

   public override void OnFixedUpdate(InputController io) { }

   // ------------------------------------------------------
   // On/Off
   // ------------------------------------------------------
   public void TogglePower()
   {
      if (powerOn)
      {
         PowerOff();
      }
      else
      {
         PowerOn();
      }
   }

   public bool PowerOn()
   {
      if (!powerOn)
      {
         powerOn = true;
         sr.sprite = onSprite;
         return true;
      }
      else
      {
         return false;
      }
   }

   public bool PowerOff()
   {
      if (powerOn)
      {
         powerOn = false;
         sr.sprite = offSprite;
         return true;
      }
      else
      {
         return false;
      }
   }

   public bool IsOn
   {
      get { return powerOn; }
   }
}
