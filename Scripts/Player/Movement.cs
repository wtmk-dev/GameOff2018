﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	
	[SerializeField]
	private float gravPower = 1f, fallGrav = 15f, jumpMod = 2f, jumpPower = 1f, speed = 5f;
	private bool isGrounded, isMoveable, isJumping, hasJumped = false;
	private float jumpTime;
	private Rigidbody rig;
	private Vector2 movement;

	private float jumpGrav = -.5f;

	void OnCollisionEnter( Collision other ){
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

	void Awake(){
		rig = GetComponent<Rigidbody>();
		isMoveable = true;
	}

	void FixedUpdate(){
		if( gameObject.activeSelf ){
			CheckInput();
		}		
	}

	private void CheckInput(){
		var horizontal = Input.GetAxisRaw( "Horizontal" );
		movement = new Vector2( horizontal, 0f );
		if( isMoveable ){
			Move( movement * speed );
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
			jumpTime += Time.fixedDeltaTime;
			rig.velocity += new Vector3( rig.velocity.x, jumpPower * jumpMod, 0f );
		}
		else if( !isJumping && !isGrounded ){
			// rig.gravityScale += fallGrav;
			// if( rig.gravityScale > gravityScale * .5 ){
			// 	rig.gravityScale = gravityScale;
			// }
			
		}

		if( Input.GetKeyDown( KeyCode.Space ) || Input.GetKeyDown("joystick button 2") ){
			if( isGrounded && !isJumping ){
				isJumping = true;
			}
		}
		
		if( Input.GetKeyUp( KeyCode.Space ) || Input.GetKeyUp("joystick button 2") ){
			isJumping = false;
			rig.velocity =new Vector2( rig.velocity.x, 0f );
		}

	}

}
