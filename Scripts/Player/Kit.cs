 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kit : MonoBehaviour {

	private bool isActive = true;

	// short range 
	// long range 
	
	// projectile
	
	// Shield
	public GameObject lShield;
	public GameObject rShield;
	private bool isBlocking;

	void Awake(){
		lShield.SetActive( false );
		rShield.SetActive( false );
	}

	void Update(){
		if( isActive ){
			DeployShield();		
		}
	}

	public void Init(){
		isActive = true;
	}

	private void DeployShield(){
		if( !isBlocking ){

			if( Input.GetKeyDown( KeyCode.Mouse0 ) ){
				lShield.SetActive( true );
				isBlocking = true;
			}
			
			if( Input.GetKeyDown( KeyCode.Mouse1 ) ){
				rShield.SetActive( true );
				isBlocking = true;
			}
		}
		else{

			if( Input.GetKeyUp( KeyCode.Mouse0 ) && !rShield.activeSelf ){
				lShield.SetActive( false );
				isBlocking = false;
			}
			
			if( Input.GetKeyUp( KeyCode.Mouse1 ) && !lShield.activeSelf ){
				rShield.SetActive( false ); 
				isBlocking = false;
			}

		}
		

	}





}
