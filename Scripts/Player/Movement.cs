using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	[SerializeField]
	private float speed = .07f;
	[SerializeField]
	private float jumpPower;

	private Rigidbody2D rig;
	private Vector2 movement;
	private bool isGrouned = false;
	public bool IsMovable{get;set;}

	void Awake(){
		rig = GetComponent<Rigidbody2D>();
		IsMovable = true;
	}

	void FixedUpdate(){
		if( gameObject.activeSelf ){
			CheckInput();
		}		
	}

	void OnCollisionEnter2D( Collision2D other ){
		if( other.gameObject.tag == "Floor" ){
			isGrouned = true;
		}
	}


	void OnCollisionExit2D( Collision2D other ) {
		if( other.gameObject.tag == "Floor" ){
			isGrouned = false;
		}
	}

	private void CheckInput(){
		var horizontal = Input.GetAxisRaw( "Horizontal" );
		var vert = 0f;
		if( isGrouned && Input.GetKey( KeyCode.Space ) ){
			vert = jumpPower;
		}
		else{
			vert = 0f;
		}
		movement = new Vector2( horizontal, vert );
		if( IsMovable ){
			Move( movement * speed );
		}

		// if(  ){
		// 	Debug.Log( "yo" );
		// 	Jump( jumpHight );
		// }
	} 

	private void Move( Vector2 movement ){
		rig.velocity = Vector2.zero;
		rig.AddForce( movement, ForceMode2D.Impulse );
	}

	private void Jump( Vector2 jump ){
		//rig.velocity = Vector2.zero;
		float jp = 0f;
	
		
	}
}
