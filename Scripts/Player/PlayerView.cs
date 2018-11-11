using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerView : MonoBehaviour {

	public static readonly string TAG = "PlayerView";
	public TextMeshProUGUI playerBlood;

	private Player player;
	private bool isActive = false;

	public void Init( Player player ){
		this.player = player;
		isActive = true;
	}

	void Update(){
		if( isActive ){
			UpdateStats();
		}
	}

	private void UpdateStats(){
		playerBlood.text = player.Blood + "";
	}
}
