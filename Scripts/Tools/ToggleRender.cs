using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRender : MonoBehaviour {

	[SerializeField]
	private GameObject target;
	[SerializeField]
	private bool destoryAfter;
	[SerializeField]
	private float killDelay = 2f;
	private MeshRenderer meshRenderer;

	void Start(){
		meshRenderer = target.GetComponent<MeshRenderer>();
	}

	void OnTriggerEnter( Collider other ){
		if( other.gameObject.tag == "Player" ){
			meshRenderer.enabled = false;
			if( destoryAfter ){
				StartCoroutine( KillDelay() );
			}
		}
	}

	private IEnumerator KillDelay(){
		yield return new WaitForSeconds( killDelay );
		Destroy( target );
	}
}
