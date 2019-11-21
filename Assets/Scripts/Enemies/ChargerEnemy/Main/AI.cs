using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargerEnemy
{
   public class AI : Enemy
   {
      // ------------------------------------------------------
      // Member Vars
      // ------------------------------------------------------
      private StateMachine<AI> machine;
      private float cooldownLeft;

      [SerializeField] private float attackCoolDown;
      [SerializeField] private float chargeForce;
      [SerializeField] public GameObject redThingy;

      // ------------------------------------------------------
      // Start
      // ------------------------------------------------------
      public override void OnValidate()
      {
         base.OnValidate();
      }

      public override void OnStart()
      {
         machine = new StateMachine(this);
         machine.ChangeState(StateMachine.IDLE);
      }

      // ------------------------------------------------------
      // Updates
      // ------------------------------------------------------
      public override void OnUpdate()
      {
         HandleCooldown();
         HandleTargeting();
         machine.OnStateUpdate();
         Debug.Log(GetCurrentStateName());
      }

      public override void OnFixedUpdate()
      {
         machine.OnStateFixedUpdate();
      }

      // ------------------------------------------------------
      // Cooldown
      // ------------------------------------------------------
      private void HandleCooldown()
      {
         cooldownLeft -= Time.deltaTime;
         cooldownLeft = Mathf.Max(0, cooldownLeft);
      }

      public bool OnCooldown()
      {
         return cooldownLeft > 0;
      }

      public void StartCooldown()
      {
         cooldownLeft = attackCoolDown;
      }

      public void EndCooldown()
      {
         cooldownLeft = 0;
      }

      // ------------------------------------------------------
      // Getters
      // ------------------------------------------------------
      public float ChargeForce
      {
         get { return chargeForce; }
      }

      public override string GetCurrentStateName()
      {
         return machine.GetStateName();
      }
   }
}
