using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour {

	[SerializeField]
	private GameObject[] target;
	[SerializeField]
	private GameObject[] offTargets;
	[SerializeField]
	private int id = -1;

	void OnEnable(){
		EventController.OnTriggerEventWithId += TriggerEvent;
	}

	void OnDisable(){
		EventController.OnTriggerEventWithId -= TriggerEvent;
	}

	void OnTriggerEnter( Collider other ){
		if( other.gameObject.tag == "Player" ){
			ToggleObjects( true, true );
			ToggleOffTarges();
		}

	}

	private void TriggerEvent( int id ){
		if( this.id == id ){
			ToggleObjects( true );
		}
	}

	private void ToggleOffTarges(){
		if( offTargets.Length > 0 ){
			foreach( GameObject go in offTargets ){
				go.SetActive( false );
			}
		}
	}

	private void ToggleObjects( bool isActive, bool destroy = false ){
		foreach( GameObject go in target ){
			go.SetActive( isActive );
		}
		
		if( destroy ){
			Destroy( gameObject );
		}
	}


}
