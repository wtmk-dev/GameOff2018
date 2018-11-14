using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

	public static event Action<int> OnTriggerEventWithId;

	private int currentId = -1;
	private int killsBeforeTrigger;

	void OnEnable(){
		ToggleKills.OnStartToggleKills += RegesterKillEvent;
		ToggleKills.OnKilled += ToggleKill;
	}

	void OnDisable(){
		ToggleKills.OnStartToggleKills -= RegesterKillEvent;
		ToggleKills.OnKilled -= ToggleKill;
	}

	private void RegesterKillEvent( int id ){
		if( currentId != id ){
			currentId = id;
			killsBeforeTrigger = 0;
		}
		killsBeforeTrigger++;
	}

	private void ToggleKill(){
		killsBeforeTrigger--;
		if( killsBeforeTrigger == 0 ){
			if( OnTriggerEventWithId != null  ){
				OnTriggerEventWithId( currentId );
			} 
		}
	}
	
}
