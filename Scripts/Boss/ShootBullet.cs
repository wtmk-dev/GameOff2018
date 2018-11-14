using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	public GameObject bullet;

	private void ShootTarget( GameObject player ){
		GameObject clone = Instantiate( bullet , transform.position, Quaternion.identity );
		clone.transform.position = Vector3.MoveTowards( clone.transform.position, player.transform.position, 5f * Time.fixedDeltaTime );

	}
}
