using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DM : MonoBehaviour {

	private static readonly System.Random random = new System.Random(); 
	private static readonly object syncLock = new object();

	private CameraController cameraController;
	private PlayerController pc;
	private DMView view;

	void Awake(){
		pc = gameObject.GetComponent<PlayerController>();
		view = GameObject.FindWithTag( DMView.TAG ).GetComponent<DMView>();
		cameraController = GameObject.FindWithTag( CameraController.TAG ).GetComponent<CameraController>();
	}

	public void StartButton() {
		view.Init( this );
		pc.Init();
		cameraController.Init( pc.GetPlayerTransform() );
	}

	public static int RandomNumber(int min, int max){
		lock(syncLock) { // synchronize
			return random.Next(min, max);
		}
	}
}
