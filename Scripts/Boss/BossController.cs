using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

	public event Action<int> OnHealthChange;
	private BossModel model;
	private BossView view;

	void Awake( ){
		view = GameObject.FindGameObjectWithTag( BossView.TAG ).GetComponent<BossView>();
		view.SetActive( false );
	}

	void OnTriggerEnter( Collider other ){
//		Debug.Log( other.gameObject.tag );
		if( other.gameObject.tag == "Whip" ){
			//Debug.Log( "it hurts" );
			LowerBloodByAmount( 1 );
		}
	}

	public void Init( BossModel model ){
		this.model = model;
		Debug.Log( model );
		view.Init( this, model.MaxHp );
		view.SetActive( true );
	}

	private void LowerBloodByAmount( int amount ){
		model.HP -= amount;
//		Debug.Log( model.HP );
		if( model.HP < 0 ){
			Debug.Log( "the world is saved...");
		}

		if( OnHealthChange != null ){
			OnHealthChange( model.HP );
		}
	}

}
