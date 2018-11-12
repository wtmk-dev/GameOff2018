using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour {

	[SerializeField]
	private GameObject[] target;

	void OnTriggerEnter( Collider other ){
		if( other.gameObject.tag == "Player" ){
			foreach( GameObject go in target ){
				go.SetActive( true );
			}
			Destroy( gameObject );
		}
	}

		


}
