using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static readonly string TAG = "Player";
	public delegate void PlayerKilled();
	public static event PlayerKilled OnPlayerKilled;
	public event Action<int> OnHealthChange;
	[SerializeField]
	[Range(0,100)]
	private int maxHp;
	private bool isInvincible, isActive;
	private Player player;
	private PlayerView view;
	private Movement movement;
	private Kit kit; 
	private LevelUpController lvlController;

	private void OnDestroy(){
		if( view != null ){
			view.OnDamageComplete -= DamageComplete;
		}
	}

	public void Init(){
		view = GameObject.FindWithTag( PlayerView.TAG ).GetComponent<PlayerView>();
		movement = GetComponent<Movement>();
		kit = GetComponent<Kit>();
		lvlController = GetComponent<LevelUpController>();
		player = new Player( maxHp );
		Debug.Log( player.Blood );
		view.Init( player, this );
		lvlController.Init( player );
		movement.Init( lvlController );
		kit.Init( lvlController, movement );

		if( view != null ){
			view.OnDamageComplete += DamageComplete;
		}

		isActive = true;
	}

	void OnCollisionEnter(Collision other) {
		if( other.gameObject.tag != "Floor" ){
			Debug.Log( other.gameObject.tag );
		}

		
	}

	void OnTriggerEnter( Collider other ){
		if( !isInvincible && other.gameObject.tag == "Boss" ){
			Debug.Log( "i was hit" );
			Debug.Log( player.Blood );
			isInvincible = true;
			LowerBloodByAmount( 1 );
		}
	}

	void Update(){
		if( isActive ){
			Kill();
		}
	}

	public Transform GetPlayerTransform(){
		return transform;
	}

	private void Kill(){
		if( player.Blood < 0 ){
			if( OnPlayerKilled != null ){
				OnPlayerKilled();
			}
			Destroy( gameObject );
		}
	}

	private void LowerBloodByAmount( int amount ){
		player.Blood -= amount;
		Debug.Log( player.Blood );
		if( player.Blood < 0 ){
			Debug.Log( "the world is doomed...");
		}

		if( OnHealthChange != null ){
			OnHealthChange( player.Blood );
		}

	}

	private void DamageComplete(){
		isInvincible = false;
	}

}
