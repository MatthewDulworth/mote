using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   private Player player;

   private EnemyController enemyController;
   private PossessionController possessControl;
   private HealthController healthController;
   private InputController io;
   private List<SceneSpecificController> sceneSpecificControllers;

   [SerializeField] private LayerMask playerLayer;


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   void Start()
   {
      // find controllers
      enemyController = FindObjectOfType<EnemyController>();
      player = FindObjectOfType<Player>();
      possessControl = FindObjectOfType<PossessionController>();
      healthController = FindObjectOfType<HealthController>();
      io = FindObjectOfType<InputController>();

      // on starts
      enemyController.OnStart();
      possessControl.OnStart();
      healthController.OnStart();

      // init scene controllers
      sceneSpecificControllers = new List<SceneSpecificController>();
      SceneSpecificController[] spcs = FindObjectsOfType<SceneSpecificController>();

      foreach (SceneSpecificController spc in spcs)
      {
         sceneSpecificControllers.Add(spc);
         spc.OnStart(enemyController);
      }

   }

   void Update()
   {
      possessControl.OnUpdate(player, io);
      enemyController.OnUpdate();
      UpdateSceneSpecificControllers();
      healthController.OnUpdate(player, possessControl.PossessedObject(), enemyController.Enemies);

      if (possessControl.CurrentlyPossessing())
      {
         possessControl.PossessedObject().OnUpdate(io);
      }
      else
      {
         player.OnUpdate();
      }
   }

   void FixedUpdate()
   {
      enemyController.OnFixedUpdate();

      if (possessControl.CurrentlyPossessing())
      {
         possessControl.PossessedObject().OnFixedUpdate(io);
      }
      else
      {
         player.HandleMovement(io);
      }
   }

   private void UpdateSceneSpecificControllers()
   {
      foreach (SceneSpecificController spc in sceneSpecificControllers)
      {
         spc.OnUpdate(enemyController, this);
      }
   }


   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public PossessionController PossessionController
   {
      get { return possessControl; }
   }

   public EnemyController EnemyController
   {
      get { return enemyController; }
   }

   public InputController InputController
   {
      get { return io; }
   }

   public HealthController HealthController
   {
      get { return healthController; }
   }

   public Player Player
   {
      get { return player; }
   }
}