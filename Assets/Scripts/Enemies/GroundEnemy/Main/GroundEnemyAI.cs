using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAI : Enemy
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool reflected;
   private float coolDownLeft = 0;
   private GroundDetector groundDetector;
   private WallAndEdgeDetector wallAndEdgeDetector;
   private StateMachine<GroundEnemyAI> machine;

   [SerializeField] private int movementDirection = 1;
   [SerializeField] private float JumpForce;
   [SerializeField] private float attackCooldownTime = 1.0f;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void OnStart(){
      wallAndEdgeDetector = GetComponent<WallAndEdgeDetector>();
      groundDetector = GetComponent<GroundDetector>();
      machine = new GE_StateMachine(this);
      reflected = false;

      if(movementDirection == -1){
         FlipHorizontal();
      }
   }

   public override void OnFixedUpdate(){
      groundDetector.DetectGround();
      wallAndEdgeDetector.DetectWalls();
      wallAndEdgeDetector.DetectEdges();

      machine.OnStateFixedUpdate();
   }

   public override void OnUpdate(){
      HandleTargeting();
      HandleCooldown();

      machine.OnStateUpdate();
   }


   // ------------------------------------------------------
   // Movement 
   // ------------------------------------------------------
   public void FlipHorizontal(){
      movementDirection *= -1;

      Vector3 newScale = transform.localScale;
      newScale.x *= -1;
      transform.localScale = newScale;

      reflected = !reflected;
      fov.ReflectOverXAxis(reflected);
      wallAndEdgeDetector.ReflectOverXAxis(reflected);
   }

   public void StopMoving(){
      rb.velocity = Vector3.zero;
   }

   public void ChangeVelocityScaled(float x, float y){
      rb.velocity = new Vector2(x * movementDirection * speed, y * JumpForce);
   }

   public void ChangeVelocityRaw(float x, float y){
      rb.velocity = new Vector2(x ,y);
   }

  
   

   // ------------------------------------------------------
   // CoolDown
   // ------------------------------------------------------
    public void HandleCooldown(){
      if(AttackOnCooldown()){
         coolDownLeft -= Time.deltaTime;
      }
   }

   public bool AttackOnCooldown(){
      return (coolDownLeft > 0) ? true : false;
   }

   public void StartCoolDown(){
      coolDownLeft = attackCooldownTime;
   }


   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public bool OnGround(){
      return groundDetector.OnGround;
   }

   public bool WallDetected(){
      return wallAndEdgeDetector.WallDetected;
   }

   public bool EdgeDetected(){
      return wallAndEdgeDetector.EdgeDetected;
   }

   public override string GetCurrentStateName(){
      return machine.GetStateName();
   }
}

