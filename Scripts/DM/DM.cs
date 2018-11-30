using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DM : MonoBehaviour {

	public delegate void GameStarted();
	public static event GameStarted OnGameStarted;
	public static Action<GameObject> OnPlayerCreated;
	public bool isDebug = false;
	private static readonly System.Random random = new System.Random(); 
	private static readonly object syncLock = new object();
	private CameraController cameraController;
	private PlayerController playerController;
	private MapController mapController;
	private DMView view;
	private GameObject goPlayer;

	void Awake(){
		goPlayer = Resources.Load( "Player" ) as GameObject;

		if( OnPlayerCreated != null ){
			OnPlayerCreated( goPlayer );
		}

		view = GameObject.FindWithTag( DMView.TAG ).GetComponent<DMView>();
		cameraController = GameObject.FindWithTag( CameraController.TAG ).GetComponent<CameraController>();
		mapController = GetComponent<MapController>();
	}

	void Start(){
		if( isDebug ){
			StartButton();
		}
	}

	public void StartButton() {
		view.Init( this );
		goPlayer = Instantiate( goPlayer , transform.position, Quaternion.identity );
		playerController = goPlayer.GetComponent<PlayerController>();
		playerController.Init();
		cameraController.Init( playerController.GetPlayerTransform() );
		view.StartScreenFade();

		if( OnGameStarted != null ){
			OnGameStarted();
		}
		
	}

	public static int RandomNumber(int min, int max){
		lock(syncLock) { // synchronize
			return random.Next(min, max);
		}
	}
}
