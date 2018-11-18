using UnityEngine;
using System.Collections;

public class TargetMovement : MonoBehaviour {

	[SerializeField]
	private float amplitude = 1f;

	[SerializeField]
	private float timePeriod = 1f;

	private Vector3 startPosition;

	[SerializeField]
	private float chanceOfMovement = 0.5f;

	// Use this for initialization
	void Start () {
	
		// Determine whether or not this target should be moving
		// if (Random.Range(0f, 1f) >= chanceOfMovement) {
		// 	this.enabled = false;
		// }

		// Store the start position
		startPosition = transform.localPosition;

	}
	

	void LateUpdate () {
	
		// Calculate theta
		float theta = Mathf.Sin(Time.timeSinceLevelLoad / timePeriod);

		// Create the new delta position
		Vector3 deltaPosition = new Vector3(0f, amplitude, 0f) * theta;

		// Update the position by adding the start position and delta position
		transform.localPosition = startPosition + deltaPosition;

	}
}
