using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<OwnerType>
{
   private OwnerType parent;

   public abstract void OnEnter();
   public abstract void OnExit();
   public abstract void OnUpdate();
   public abstract void OnFixedUpdate();
}
