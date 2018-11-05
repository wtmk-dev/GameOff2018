using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMView : MonoBehaviour {

	public static readonly string TAG = "DMView";
	private DM controller;
	public GameObject startButton;

	public void Init( DM controller ){
		this.controller = controller;
		startButton.SetActive( false );
	}

	public void ActivateUI( string name, bool isActive ){
		if( name == "Start" ){
			startButton.SetActive( isActive );
		}
	}
}
