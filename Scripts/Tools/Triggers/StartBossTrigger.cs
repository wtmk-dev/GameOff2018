using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossTrigger : MonoBehaviour {

	public delegate void BossStart( GameObject player );
	public static event BossStart OnBossStart;
	public static bool isActive = false;

	void OnCollisionEnter( Collision other ){
		if( !isActive ){
			if( other.gameObject.tag == "Player" ){
				isActive = true;
				if( OnBossStart != null ){
					OnBossStart( other.gameObject );
				}
			}
		}
		
	}
}
