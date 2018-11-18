using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	[SerializeField]
	private GameObject bullet;
	[SerializeField]
	private bool left,right,up,down = false;
	[SerializeField]
	[Range(0,100)]
	private float timeBetweenShots, speed, destroyAfter;
	private MoveTransformOnAxis moveTransform;

	void Start(){
		StartCoroutine( SpawnBullet() );
	}

	private void Spawn(){
		GameObject clone = Instantiate( bullet , transform.position, Quaternion.identity );
		moveTransform = clone.GetComponent<MoveTransformOnAxis>();
		moveTransform.Init( left, right, up, down, speed, destroyAfter );
	}

	private IEnumerator SpawnBullet(){
		float elapsed = 0;
		while( elapsed < timeBetweenShots ){
			elapsed += Time.deltaTime;
			yield return null;
		}
		OnSpawnBulletComplete();
	}

	private void OnSpawnBulletComplete(){
		Spawn();
		StopCoroutine( SpawnBullet() );
		StartCoroutine( SpawnBullet() );
	}


}
