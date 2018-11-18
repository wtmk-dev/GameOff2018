using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformOnAxis : MonoBehaviour {

	private bool left,right,up,down = false;
	private float destroyAfter;
	private float speed; 

	void OnDisable(){
		StopAllCoroutines();
	}

	void Start(){
		StartCoroutine( MoveThenDestory() );
	}

	public void Init( bool left, bool right, bool up, bool down, float speed, float destroyAfter ){
		this.left = left; this.right = right; this.up = up; this.down = down;
		this.speed = speed; this.destroyAfter = destroyAfter;
	}
	private IEnumerator MoveThenDestory(){
		float elapsed = 0;

		while( elapsed < destroyAfter ){
			elapsed += Time.deltaTime;
			if( left ){
				transform.Translate( Vector3.left * Time.deltaTime * speed );
			}
			else if( right ){
				transform.Translate( Vector3.right * Time.deltaTime * speed );
			}
			else if( up ){
				transform.Translate( Vector3.up * Time.deltaTime * speed );
			}
			else if( down ){
				transform.Translate( Vector3.down * Time.deltaTime * speed );
			}
			yield return null;
		}

		Destroy( gameObject );

	}





}
