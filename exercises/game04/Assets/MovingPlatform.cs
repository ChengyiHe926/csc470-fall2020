using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	float freq = 1f;
	float amp = 3f;

	float theta = 0;

	Vector3 startPosition;
	Vector3 previousPosition;
	public Vector3 DistanceMoved = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
		startPosition = transform.position;
		previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		theta += Time.deltaTime;

		Vector3 newPos = startPosition + transform.forward * Mathf.Sin(theta * freq) * amp;
		transform.position = newPos;
		
		// Store the distanced we have moved so that anything "riding" the platform will know how far to move.
		DistanceMoved = transform.position - previousPosition;

		previousPosition = transform.position;
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) {
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			player.PlatformAttachedTo = this;
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player")) {
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			player.PlatformAttachedTo = null;
		}
	}
}
