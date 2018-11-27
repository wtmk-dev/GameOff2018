using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {

	public bool isReflectable;

	private void OnTriggerEnter(Collider other) {
		IProjectile testInterface = (IProjectile) other.GetComponent<IProjectile>();
		Debug.Log( testInterface );
		if( testInterface != null ){
			if( testInterface.Reflectable && isReflectable ){
				Debug.Log( "I WAS HIT" );
                Shootable testShot = other.GetComponent<Shootable>();
                testShot.ReversePostions();//call bullets reflect
			}
		}
	}

}
