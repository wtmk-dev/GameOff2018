using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

	private Shootable[] shootables;
	private List<Transform> goShootables;

	private Vector3 startPos;

	public void Init(){
		shootables = FindObjectsOfType<Shootable>();
		goShootables = new List<Transform>();
		foreach (var item in shootables) {
			goShootables.Add( item.getTransform() );
		}
	}

	public void Fire(){
		startPos = transform.position;
		Debug.Log( "FIRE" );
		StartCoroutine( MoveToTarget() );
	}

	private IEnumerator MoveToTarget(){
		Debug.Log( "this is the broken code" );
		float step = 10f * Time.deltaTime;
		float elapsed = 0f;
		do{
			elapsed += Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, goShootables[0].position, step);
			yield return null;
		}while( elapsed < 10f );
		transform.position = startPos;
	}
}
