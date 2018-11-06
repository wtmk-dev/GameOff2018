using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	public GameObject bullet;

	private void ShootBullet1(){
		Instantiate( bullet , transform.position, Quaternion.identity );
//		bullet.GetComponent<Rigidbody>().velocity += new Vector3( 10f, rig.velocity.y, 0f );
	}
}
