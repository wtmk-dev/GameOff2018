using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	public static readonly string TAG = "CameraController";

//holds the value of player game object
	[SerializeField]
	private Transform player;

	[SerializeField]
	private Vector3 offset = new Vector3();

//speed of camera
	[SerializeField]
	private float cameraSpeed = 0f;

	// Update is called once per frame
	void FixedUpdate () {
		if( player != null ){
			Vector3 currentPos = player.transform.position + offset;
			Vector3 toPos = new Vector3( currentPos.x, offset.y, offset.z );
			transform.position = Vector3.Lerp(transform.position, toPos, Time.fixedDeltaTime * cameraSpeed);
		} 
		
	}

	public void Init( Transform player ){
		this.player = player;
		cameraSpeed = 100f;
	}



}
