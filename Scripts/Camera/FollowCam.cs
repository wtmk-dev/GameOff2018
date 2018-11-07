using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowCam : MonoBehaviour {

//holds the value of player game object
	[SerializeField]
	private Transform player;

	[SerializeField]
	private Vector3 offset = new Vector3();

//speed of camera
	[SerializeField]
	private float cameraSpeed;

	// Update is called once per frame
	void FixedUpdate () {
		if( player != null ){
			cameraSpeed = 100;
			Vector3 currentPos = player.transform.position + offset;
			Vector3 toPos = new Vector3( currentPos.x, offset.y, offset.z );
			transform.position = Vector3.Lerp(transform.position, toPos, Time.fixedDeltaTime * cameraSpeed);
		} 
		else {
			cameraSpeed = 0;
		}
		
	}



}
