using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMove : MonoBehaviour {

	[SerializeField]
	private int id;
	private bool isActive;

	[SerializeField]
	private float amplitude = 2f;

	[SerializeField]
	private float timePeriod = 1f;

	private Vector3 startPosition;

	void OnEnable(){
		EventController.OnTriggerEventWithId += Move;
	}

	void OnDisable(){
		EventController.OnTriggerEventWithId -= Move;
	}

	void Start () {
		startPosition = transform.localPosition;
		startPosition.y = 3;
	}

	private void Move( int id ){
		if( this.id == id ){
			isActive = true;
		}
	}

	void LateUpdate () {
		if( isActive ){
			float theta = Mathf.Sin(Time.timeSinceLevelLoad / timePeriod);
			Vector3 deltaPosition = new Vector3(0f, amplitude, 0f) * theta;
			transform.localPosition = startPosition + deltaPosition;
		}
	}



}
