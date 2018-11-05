using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DM : MonoBehaviour {
	
	private PlayerController pc;
	private DMView view;

	void Awake(){
		pc = gameObject.GetComponent<PlayerController>();
		view = GameObject.FindWithTag( DMView.TAG ).GetComponent<DMView>();
	}

	public void StartButton() {
		view.Init( this );
		pc.Init();
	}
}
