 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kit : MonoBehaviour {

	private enum Stance { LRANGE, SRANGE }
	private Stance stance;
	private bool stanceChanged = false;

	private bool isActive = false;

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

	private LevelUpController lvlController;

	void Awake(){
		lShield.SetActive( false );
		rShield.SetActive( false );
		stance = Stance.LRANGE;
	}

	void Update(){
		if( isActive ){
			DeployShield();
			DeployWhip();
			DeploySword();	
			DeployShoot();
			SwapStance();	
		}
	}

	public void Init( LevelUpController lvlController ){
		this.lvlController = lvlController;
		isActive = true;
	}

	private void DeployShield(){
		if( !isBlocking ){
			if( Input.GetKeyDown( KeyCode.Mouse0 ) && !isAttacking || Input.GetKeyDown("joystick button 4") ){
				lShield.SetActive( true );
				isBlocking = true;
			}
			
			if( Input.GetKeyDown( KeyCode.Mouse1 ) && !isAttacking || Input.GetKeyDown("joystick button 5") ){
				rShield.SetActive( true );
				isBlocking = true;
			}
		}
		else if( isBlocking && !isAttacking ){
			if( Input.GetKeyUp( KeyCode.Mouse0 ) && !rShield.activeSelf || Input.GetKeyUp("joystick button 4") && !rShield.activeSelf ){
				lShield.SetActive( false );
				isBlocking = false;
			}
			
			if( Input.GetKeyUp( KeyCode.Mouse1 ) && !lShield.activeSelf || Input.GetKeyUp("joystick button 5") && !lShield.activeSelf ){
				rShield.SetActive( false ); 
				isBlocking = false;
			}


		}
		

	}

	private void DeployWhip(){
		if( !isAttacking && stance == Stance.LRANGE ){
			if( Input.GetKeyDown( KeyCode.Q ) && !isBlocking || Input.GetKeyDown("joystick button 3") && !isBlocking ){
				lWhip.SetActive( true );
				isAttacking = true;
			}
			if( Input.GetKeyDown( KeyCode.E ) && !isBlocking || Input.GetKeyDown("joystick button 1") && !isBlocking ){
				rWhip.SetActive( true );
				isAttacking = true;
			}
			

		}
		else if( stance == Stance.LRANGE ){
			if( Input.GetKeyUp( KeyCode.Q ) && !rWhip.activeSelf || Input.GetKeyUp("joystick button 3") && !rWhip.activeSelf ){
				lWhip.SetActive( false );
				isAttacking = false;
			}
			if( Input.GetKeyUp( KeyCode.E ) && !lWhip.activeSelf || Input.GetKeyUp("joystick button 1") && !lWhip.activeSelf ){
				rWhip.SetActive( false );
				isAttacking = false;
			}


		}
		

	}

	private void DeploySword(){
		if( !isAttacking && stance == Stance.SRANGE ){
			if( Input.GetKeyDown( KeyCode.Z ) && !isBlocking || Input.GetKeyDown("joystick button 3") && !isBlocking ){
				lSword.SetActive( true );
				isAttacking = true;
			}
			if( Input.GetKeyDown( KeyCode.C ) && !isBlocking || Input.GetKeyDown("joystick button 1") && !isBlocking ){
				rSword.SetActive( true );
				isAttacking = true;
			}
			

		}
		else if( stance == Stance.SRANGE ) {
			if( Input.GetKeyUp( KeyCode.Z ) && !rSword.activeSelf || Input.GetKeyUp("joystick button 3") && !rSword.activeSelf ){
				lSword.SetActive( false );
				isAttacking = false;
			}
			if( Input.GetKeyUp( KeyCode.C ) && !lSword.activeSelf || Input.GetKeyUp("joystick button 1") && !lSword.activeSelf ){
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

	private void SwapStance(){
		var vertical = Input.GetAxisRaw( "Vertical" );
		if( vertical == 1 ){
			stance = Stance.LRANGE;
		}

		if( vertical == -1 ){
			stance = Stance.SRANGE;
		}
		
	}

















}
