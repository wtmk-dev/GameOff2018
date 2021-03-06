﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Player player;
	private PlayerView view;
	private GameObject prefabPlayer;
	private GameObject goPlayer;

	private Movement movement;
	private Kit kit; 

	void Awake(){
		goPlayer = Resources.Load( "Player" ) as GameObject;
	}

	public void Init(){
		goPlayer = Instantiate( goPlayer , transform.position, Quaternion.identity );
		movement = goPlayer.GetComponent<Movement>();
		kit = goPlayer.GetComponent<Kit>();
		player = new Player();
	}

}
