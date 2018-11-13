using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleKills : MonoBehaviour {

	public static event Action<int> OnStartToggleKills;
	public delegate void Killed();
	public static event Killed OnKilled;

	[SerializeField]
	private int id = -1;

	void OnEnable(){
	}

	void OnDisable(){
	}

	void Start(){
		if( OnStartToggleKills != null ){
			OnStartToggleKills( id );
		}		
	}

	void OnDestroy(){
		if( OnKilled != null ){
			OnKilled();
		}
	}


}
