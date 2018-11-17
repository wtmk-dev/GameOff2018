 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kit : MonoBehaviour {

	private bool isActive = false;

	// short range 
	public GameObject lSword;
	private GameObject goSwordReady;
	private ParticleSystem swordReadyFX;
	private bool isSwording;

	// long range 
	public GameObject lWhip;
	private bool isAttacking;

	// projectile
	public GameObject shoot;
	private bool canShoot;

	// Shield
	public GameObject lShield;
	private bool isBlocking;

	private float dpad;

	private LevelUpController lvlController;
	private Movement movement;

	void Awake(){
		LoadResources();
		DeactivateAll();
	}

	void Update(){
		if( isActive ){
			dpad = Input.GetAxisRaw( "Vertical" );
			DeployShield();
			DeployWhip();
			DeploySword();	
			DeployShoot();
		}
	}

	public void Init( LevelUpController lvlController, Movement movement ){
		this.lvlController = lvlController;
		this.movement = movement;
		isActive = true;
	}

	private void LoadResources(){
		goSwordReady = Resources.Load( "SwordReadyFX" ) as GameObject;
		goSwordReady = Instantiate( goSwordReady, transform.position, Quaternion.identity );
		goSwordReady.transform.parent = transform;
		swordReadyFX = goSwordReady.GetComponent<ParticleSystem>();
	}

	private void DeactivateAll(){
		lShield.SetActive( false );
		lSword.SetActive( false );
		lWhip.SetActive( false );
	}
	private void DeployShield(){
		if( !isBlocking ){
			if( Input.GetKeyDown( KeyCode.Mouse0 ) && !isAttacking || Input.GetKeyDown("joystick button 4") ){
				lShield.SetActive( true );
				isBlocking = true;
			}
			
		}
		else if( isBlocking && !isAttacking ){
			if( Input.GetKeyUp( KeyCode.Mouse0 ) || Input.GetKeyUp("joystick button 4") ){
				lShield.SetActive( false );
				isBlocking = false;
			}

		}

	}

	private void DeployWhip(){
		if( !isAttacking && !isBlocking ){

			if( Input.GetKeyDown( KeyCode.Q ) && dpad == 0 || Input.GetKeyDown("joystick button 1") && dpad == 0 ){
				StartCoroutine( WhipAttack( true ) );
			}
			

		}
		else if( isAttacking ){
			if( Input.GetKeyUp( KeyCode.Q ) || Input.GetKeyUp("joystick button 3") ){
				lWhip.SetActive( false );
				isAttacking = false;
			}

		}
		
	}

	private IEnumerator WhipAttack( bool isLeft = false ){
		yield return new WaitForEndOfFrame();
		isAttacking = true;
		if( isLeft ){
			lWhip.SetActive( true );
		}else{
		//	rWhip.SetActive( true );
		}
		yield return new WaitForSeconds( lvlController.GetWhip() );
		if( isLeft ){
			lWhip.SetActive( false );
		}else{
		//	rWhip.SetActive( false );
		}
		yield return new WaitForSeconds( lvlController.GetWhip() );
		OnWhipAttackComplete();
	}

	private void OnWhipAttackComplete(){
		isAttacking = false;
		StopCoroutine( WhipAttack() );
	}

	private void DeploySword(){
		if( !isSwording && !isBlocking ){
			if( Input.GetKeyDown( KeyCode.E ) && dpad >= 1 || Input.GetKeyDown("joystick button 1") && dpad >= 1 ){
				StartCoroutine( SwordAttack( true ) );
			}
		}
		else if( isSwording ) {
			if( Input.GetKeyUp( KeyCode.E ) || Input.GetKeyUp("joystick button 1") ){
				lSword.SetActive( false );
			}
		}
	}

	private IEnumerator SwordAttack( bool isLeft = false ){
		yield return new WaitForEndOfFrame();
		isSwording = true;
		if( isLeft ){
			lSword.SetActive( true );
		}
		else{
			//rSword.SetActive( true );
		}
		movement.JumpSlash();
		yield return new WaitForSeconds( lvlController.GetSword() / 2 );
		if( isLeft ){
			lSword.SetActive( false );
		}
		else{
			//rSword.SetActive( false );
		}
		yield return new WaitForSeconds( lvlController.GetSword() / 2 );
		OnSwordAttackComplete();
	}
	
	private void OnSwordAttackComplete(){
		isSwording = false;
		swordReadyFX.Play();
		StopCoroutine( SwordAttack() );
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
