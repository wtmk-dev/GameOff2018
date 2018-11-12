using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	
	[SerializeField]
	private float gravPower = 1f, fallGrav = 15f, jumpMod = 2f, jumpPower = 1f;
	private bool isInit, isGrounded, isMoveable, isJumping, hasJumped = false;
	private float jumpTime;
	private Rigidbody rig;
	private Vector2 movement;

	private float jumpGrav = -.5f;
	
	private LevelUpController lvlController;

	// void OnCollisionStay( Collision other ){
	// 	//Debug.Log( "can jump" );
	// 	if( other.gameObject.tag == "Floor" ){
	// 		isGrounded = true;
	// 		isJumping = false;
	// 		hasJumped = false;
	// 		jumpTime = 0f;
	// 	}
	// }

	void OnCollisionEnter( Collision other ){
		//Debug.Log( "can jump" );
		if( other.gameObject.tag == "Floor" ){
			isGrounded = true;
			isJumping = false;
			hasJumped = false;
			jumpTime = 0f;
		}
	}


	void OnCollisionExit( Collision other ) {
		if( other.gameObject.tag == "Floor" ){
			isGrounded = false;
		}
	}

	void FixedUpdate(){
		if( isInit ){
			CheckInput();
		}		
	}

	public void Init( LevelUpController lvlController ){
		this.lvlController = lvlController;
		rig = GetComponent<Rigidbody>();
		isMoveable = true;
		isInit = true;
	}

	public void Kockback(){
		Debug.Log( "kockback" );
	}

	private void CheckInput(){
		var horizontal = Input.GetAxisRaw( "Horizontal" );
		movement = new Vector2( horizontal, 0f );
		if( isMoveable ){
			Move( movement * lvlController.GetSpeed() );
			Jump();
		}
	}

	private void Move( Vector2 movement ){
		rig.velocity = new Vector2( 0f, rig.velocity.y );
		rig.AddForce( movement, ForceMode.Impulse );
	}

	private void Jump(){
		if( jumpTime > gravPower ){ 
			isJumping = false;
		}

		if( isJumping ){
			Debug.Log( lvlController.GetSpeed() );
			jumpTime += Time.fixedDeltaTime;
			rig.velocity += new Vector3( rig.velocity.x, lvlController.GetJump() * jumpMod, 0f );
		}
		else if( !isJumping && !isGrounded ){
			isGrounded = true; // maybe change to check if is grounded
		}

		if( Input.GetKeyDown( KeyCode.Space ) || Input.GetKeyDown("joystick button 2") ){
			if( isGrounded && !isJumping ){
				isJumping = true;
				lvlController.JumpExp();
			}
		}
		
		if( Input.GetKeyUp( KeyCode.Space ) && isJumping || Input.GetKeyUp("joystick button 2") && isJumping ){
			isJumping = false;
			rig.velocity = new Vector2( rig.velocity.x, 0f );
		}

	}

	public void JumpSlash(){
		rig.velocity += new Vector3( rig.velocity.x, lvlController.GetJump() * jumpMod, 0f );
	}

	public void ZeroVelocity(){
		rig.velocity = Vector3.zero;
	}

}
