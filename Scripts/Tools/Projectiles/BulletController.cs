using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	void OnTriggerEnter( Collider other ){
		if( other.gameObject.tag == Kit.SHIELD ){
			Destroy( gameObject );
		}
	}
}
