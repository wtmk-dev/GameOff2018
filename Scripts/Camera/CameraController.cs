using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	public static readonly string TAG = "CameraController";
	public bool allowMoveOnY;
//holds the value of player game object
	[SerializeField]
	private Transform player;
	[SerializeField]
	private Vector3 offset = new Vector3();
	[SerializeField]
	private float cameraSpeed = 0f;

	void OnEnable(){
		EventController.OnTriggerEventWithId += ActivateBossCam;
		StartBossTrigger.OnBossStart += ActivateBossCamWithGo;
	}

	void OnDisable(){
		EventController.OnTriggerEventWithId -= ActivateBossCam;
		StartBossTrigger.OnBossStart -= ActivateBossCamWithGo;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if( player != null ){
			Vector3 currentPos = player.transform.position + offset;
			
			if( allowMoveOnY ){
				Vector3 toPosWithY = new Vector3( currentPos.x, currentPos.y, offset.z );
				transform.position = Vector3.Lerp(transform.position, toPosWithY, Time.fixedDeltaTime * cameraSpeed);
			}
			else{
				Vector3 toPos = new Vector3( currentPos.x, offset.y, offset.z );
				transform.position = Vector3.Lerp(transform.position, toPos, Time.fixedDeltaTime * cameraSpeed);
			}

		} 
		
	}

	public void Init( Transform player ){
		this.player = player;
		cameraSpeed = 100f;
		allowMoveOnY = false;
	}

	private void ActivateBossCam( int id ){
		if( id == 1 ){
			cameraSpeed = 5f;
			allowMoveOnY = true;
		}
	}

	private void ActivateBossCamWithGo( GameObject id ){
		if( id.tag == "Player" ){
			cameraSpeed = 5f;
			allowMoveOnY = true;
		}
	}




}
