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

	private string floorTag = "Floor";
	
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
		//Debug.Log( "OnCollisionEnter" );
		if( other.gameObject.tag == floorTag ){
			isGrounded = true;
			isJumping = false;
		}
	}

	void OnCollisionStay( Collision other ){
		//Debug.Log( "OnCollisionStay" );
		if( other.gameObject.tag == floorTag && !isJumping ){
			isGrounded = true;
		}
	}	


	void OnCollisionExit( Collision other ) {
		//Debug.Log( "OnCollisionExit" );
		isGrounded = false;
	}

	void FixedUpdate(){
		if( isInit ){
			CheckInput();
		}
	
		// if( !isGrounded ){
		// 	Debug.Log( "isGrounded:" + isGrounded );
		// 	Debug.Log( "isJumping: " + isJumping );
		// 	Debug.Log( "hasJumped: " + hasJumped );	
		// }
		
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
		rig.velocity = new Vector2( .025f, rig.velocity.y );
		rig.AddForce( movement, ForceMode.Impulse );
	}

	private void Jump(){
		if( Input.GetKey( KeyCode.Space ) || Input.GetKey("joystick button 2") ){
			if( isGrounded && !isJumping ){
				lvlController.JumpExp();
				StartCoroutine( JumpOverTime() );
			}
		}
		
		if( Input.GetKeyUp( KeyCode.Space ) && isJumping || Input.GetKeyUp("joystick button 2") && isJumping ){
			rig.velocity = new Vector3( rig.velocity.x, 0f, 0f );
		}

	}

	private IEnumerator JumpOverTime(){
		isJumping = true;
		isGrounded = false;
        float elapsed = 0f;
		float x = rig.velocity.x;
		float y = rig.velocity.y;
        while( elapsed < gravPower ){
            elapsed += Time.fixedDeltaTime;
			float jump = lvlController.GetJump() * jumpMod;
			float ljump = Mathf.Lerp( y, jump, elapsed / gravPower );
            rig.velocity += new Vector3( x, ljump, 0f );
            yield return null;
        }
       OnJumpOverTimeComplete();
    }

	private void OnJumpOverTimeComplete(){
		isJumping = false;
	}

	public void JumpSlash(){
		rig.velocity += new Vector3( rig.velocity.x, lvlController.GetJump() * jumpMod, 0f );
	}

	public void ZeroVelocity(){
		rig.velocity = Vector3.zero;
	}

}
