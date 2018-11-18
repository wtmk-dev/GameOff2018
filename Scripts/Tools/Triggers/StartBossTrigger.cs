using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossTrigger : MonoBehaviour {

	public delegate void BossStart( GameObject player );
	public static event BossStart OnBossStart;

	void OnCollisionEnter( Collision other ){
		if( other.gameObject.tag == "Player" ){
			if( OnBossStart != null ){
				OnBossStart( other.gameObject );
			}
		}
	}
}
