using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Player player;
	private PlayerView view;
	private Movement movement;
	private Kit kit; 
	private LevelUpController lvlController;

	public void Init(){
		view = GameObject.FindWithTag( PlayerView.TAG ).GetComponent<PlayerView>();
		movement = GetComponent<Movement>();
		kit = GetComponent<Kit>();
		lvlController = GetComponent<LevelUpController>();
		player = new Player();

		view.Init( player );
		lvlController.Init( player );
		movement.Init( lvlController );
		kit.Init( lvlController, movement );
	}

	public Transform GetPlayerTransform(){
		return transform;
	}

}
