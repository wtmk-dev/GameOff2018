 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kit : MonoBehaviour {

	private bool isActive = true;

	// short range 
	public GameObject lSword;
	public GameObject rSword;

	// long range 
	public GameObject lWhip;
	public GameObject rWhip;
	private bool isAttacking;

	// projectile
	public GameObject shoot;
	private bool canShoot = true;

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
			DeployWhip();
			DeploySword();	
			DeployShoot();	
		}
	}

	public void Init(){
		isActive = true;
	}

	private void DeployShield(){
		if( !isBlocking ){
			if( Input.GetKeyDown( KeyCode.Mouse0 ) && !isAttacking ){
				lShield.SetActive( true );
				isBlocking = true;
			}
			
			if( Input.GetKeyDown( KeyCode.Mouse1 ) && !isAttacking ){
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

	private void DeployWhip(){
		if( !isAttacking ){
			if( Input.GetKeyDown( KeyCode.Q ) && !isBlocking ){
				lWhip.SetActive( true );
				isAttacking = true;
			}
			if( Input.GetKeyDown( KeyCode.E ) && !isBlocking ){
				rWhip.SetActive( true );
				isAttacking = true;
			}
			

		}
		else{
			if( Input.GetKeyUp( KeyCode.Q ) && !rWhip.activeSelf ){
				lWhip.SetActive( false );
				isAttacking = false;
			}
			if( Input.GetKeyUp( KeyCode.E ) && !lWhip.activeSelf ){
				rWhip.SetActive( false );
				isAttacking = false;
			}


		}
		

	}

	private void DeploySword(){
		if( !isAttacking ){
			if( Input.GetKeyDown( KeyCode.Z ) && !isBlocking ){
				lSword.SetActive( true );
				isAttacking = true;
			}
			if( Input.GetKeyDown( KeyCode.C ) && !isBlocking ){
				rSword.SetActive( true );
				isAttacking = true;
			}
			

		}
		else{
			if( Input.GetKeyUp( KeyCode.Z ) && !rSword.activeSelf && !lWhip.activeSelf && !rWhip.activeSelf ){
				lSword.SetActive( false );
				isAttacking = false;
			}
			if( Input.GetKeyUp( KeyCode.C ) && !lSword.activeSelf && !lWhip.activeSelf && !rWhip.activeSelf ){
				rSword.SetActive( false );
				isAttacking = false;
			}


		}
		

	}

	private void DeployShoot(){
		if( canShoot ){
			if( Input.GetKeyDown( KeyCode.B ) ){
				shoot.SetActive( true );
				canShoot = false;
			}
		
		}
		

	}

















}
