using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<OwnerType>
{
   protected OwnerType owner;
   protected StateMachine<OwnerType> machine;

   public virtual void OnEnter() { }
   public virtual void OnExit() { }
   public abstract void OnUpdate();
   public abstract void OnFixedUpdate();
   public abstract void HandleStateChanges();
}
