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

	[SerializeField] private LayerMask playerLayer;


	// ------------------------------------------------------
	// Mono Methods
	// ------------------------------------------------------
	void Start()
	{
		enemyController = FindObjectOfType<EnemyController>();
		player = FindObjectOfType<Player>();
		possessControl = FindObjectOfType<PossessionController>();
		healthController = FindObjectOfType<HealthController>();
		io = FindObjectOfType<InputController>();

		enemyController.OnStart();
		possessControl.OnStart();
		healthController.OnStart();
	}

	void Update()
	{
		possessControl.OnUpdate(player, io);
		enemyController.OnUpdate();
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


	// ------------------------------------------------------
	// Public Methods
	// ------------------------------------------------------
	public void ForceUnpossession()
	{
		possessControl.ForcedUnpossession(player);
	}
}