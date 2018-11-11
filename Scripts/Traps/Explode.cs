using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

	[SerializeField]
	private float blastMax = 25f, blastMin = 5f;
	private bool isDetonate = false;
	private SphereCollider blastRadius;

	void Awake(){
		blastRadius = GetComponent<SphereCollider>();
		blastRadius.radius = blastMin;
	}

	void OnCollisionEnter( Collision other ) {
		if( !isDetonate ){
			if( other.gameObject.tag == "Player" ){
				Movement move = other.gameObject.GetComponent<Movement>();
				move.Kockback();
			}
			StartCoroutine( Detonate() );
		}
	}
	void OnTriggerEnter(Collider other) {
		if( !isDetonate ){
			//StartCoroutine( Detonate() );
			Destroy( gameObject );
		}
	}

	private IEnumerator Detonate(){
		yield return new WaitForEndOfFrame();
		isDetonate = true;

		do{
			blastRadius.radius += 1f;
			yield return new WaitForSeconds( Time.fixedDeltaTime );
		}while( blastRadius.radius < blastMax );

		yield return new WaitForEndOfFrame();
		OnDetonateComplete();
	}

	private void OnDetonateComplete(){
		isDetonate = false;
		StopCoroutine( Detonate() );
		Destroy( gameObject );
	}

	

}
