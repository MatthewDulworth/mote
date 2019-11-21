using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(WallAndEdgeDetector))]
public class GroundEnemyAI : Enemy
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private bool facingLeft;
   private float coolDownLeft = 0;
   private GroundDetector groundDetector;
   private WallAndEdgeDetector wallAndEdgeDetector;
   private StateMachine<GroundEnemyAI> machine;

   [SerializeField] private int movementDirection = 1;
   [SerializeField] private float jumpForce;
   [SerializeField] private float jumpAttackCoolDown = 1.0f;

   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public override void OnStart()
   {
      wallAndEdgeDetector = GetComponent<WallAndEdgeDetector>();
      groundDetector = GetComponent<GroundDetector>();
      machine = new GE_StateMachine(this);
      facingLeft = false;

      if (movementDirection == -1)
      {
         FlipHorizontal();
      }
   }

   public override void OnFixedUpdate()
   {
      groundDetector.DetectGround();
      wallAndEdgeDetector.DetectWalls();
      wallAndEdgeDetector.DetectEdges();

      machine.OnStateFixedUpdate();
   }

   public override void OnUpdate()
   {
      HandleTargeting();
      machine.OnStateUpdate();
   }


   // ------------------------------------------------------
   // Movement 
   // ------------------------------------------------------
   public void FlipHorizontal()
   {
      movementDirection *= -1;

      Vector3 newScale = transform.localScale;
      newScale.x *= -1;
      transform.localScale = newScale;

      facingLeft = !facingLeft;
      fov.ReflectOverXAxis(facingLeft);
      wallAndEdgeDetector.ReflectOverXAxis(facingLeft);
   }

   public void StopMoving()
   {
      rb.velocity = Vector3.zero;
   }

   public void ChangeVelocityScaled(float x, float y)
   {
      rb.velocity = new Vector2(x * movementDirection * speed, y * jumpForce);
   }

   public void MoveToTargetX()
   {
      if (currentTarget != null)
      {

         Vector3 targetPosition = new Vector3(currentTarget.position.x, transform.position.y, transform.position.z);
         transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
      }
      else
      {
         Debug.LogError("There is something wrong with your code, this shouldn't happen big boy.");
      }
   }


   // ------------------------------------------------------
   // Checks
   // ------------------------------------------------------
   public bool OnGround()
   {
      return groundDetector.OnGround;
   }

   public bool WallDetected()
   {
      return wallAndEdgeDetector.WallDetected;
   }

   public bool EdgeDetected()
   {
      return wallAndEdgeDetector.EdgeDetected;
   }

   public int TargetOnLeftOrRight()
   {
      if (TargetSighted())
      {
         return fov.TargetOnLeftOrRight(currentTarget);
      }
      else
      {
         return -100;
      }
   }


   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public override string GetCurrentStateName()
   {
      return machine.GetStateName();
   }

   public float JumpAttackCoolDown
   {
      get { return jumpAttackCoolDown; }
   }

   public bool FacingLeft
   {
      get { return facingLeft; }
   }

   public float JumpSpeed
   {
      get { return jumpForce; }
   }

   public int MovementDirection
   {
      get { return movementDirection; }
   }
}

