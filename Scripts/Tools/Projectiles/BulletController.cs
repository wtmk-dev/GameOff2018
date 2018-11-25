using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IProjectile {

	public bool Reflectable {get;set;}

	void Awake(){
		Reflectable = true;
	}

	void OnTriggerEnter( Collider other ){
		if( other.gameObject.tag == Kit.SHIELD ){
			Destroy( gameObject );
		}
	}
}
