using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	
	[SerializeField]
	private float gravityScale, gravPower = 1f, jumpGrav = -.5f, fallGrav = 15f, jumpMod = 2f, jumpPower = 1f, speed = 5f;
	private bool isGrounded, isMoveable, isJumping, hasJumped = false;
	private float jumpTime;
	private Rigidbody2D rig;
	private Vector2 movement;

	void OnCollisionEnter2D( Collision2D other ){
		if( other.gameObject.tag == "Floor" ){
			isGrounded = true;
			isJumping = false;
			hasJumped = false;
			jumpTime = 0f;
			rig.gravityScale = gravityScale;
		}
	}


	void OnCollisionExit2D( Collision2D other ) {
		if( other.gameObject.tag == "Floor" ){
			isGrounded = false;
		}
	}

	void Awake(){
		rig = GetComponent<Rigidbody2D>();
		isMoveable = true;
		gravityScale = rig.gravityScale;
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
		rig.AddForce( movement, ForceMode2D.Impulse );
	}

	private void Jump(){
		if( jumpTime > gravPower ){
			isJumping = false;
		}

		if( isJumping ){
			jumpTime += Time.fixedDeltaTime;
			rig.gravityScale -= jumpGrav;
			rig.velocity += new Vector2( rig.velocity.x, jumpPower * jumpMod );
		}
		else if( !isJumping && !isGrounded ){
			rig.gravityScale += fallGrav;
			// if( rig.gravityScale > gravityScale * .5 ){
			// 	rig.gravityScale = gravityScale;
			// }
		}

		if( Input.GetKeyDown( KeyCode.Space ) ){
			if( isGrounded && !isJumping ){
				isJumping = true;
			}
		}
		
		if( Input.GetKeyUp( KeyCode.Space ) ){
			isJumping = false;
		}
		
		

	}

}
