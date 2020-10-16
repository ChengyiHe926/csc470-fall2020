using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
	public Rigidbody rb;

	public GameObject applePiePrefab;

	public GameObject player;
	public GameObject PlatformFather;


	float speed = 10;
	float maxSpeed = 20;
	float forwardSpeed = 8;
	float pitchSpeed = 80;
	float pitchModSpeedRate = 8f;
	float rollSpeed = 80;

	int score = 0;
	
    // Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");

		// Rotate the plane based on input.
		float xRot = vAxis * pitchSpeed * Time.deltaTime;
		float yRot = hAxis * rollSpeed / 4 * Time.deltaTime;
		float zRot = -hAxis * rollSpeed * Time.deltaTime;
		transform.Rotate(xRot, yRot, zRot, Space.Self);

		// Compute a modifier (forwardSpeed) based on if the plane is looking up or down.
		// If the y value of the tranform's forward is positive, we know we are looking up, 
		// if it is negative, we know we are looking down.
		forwardSpeed += -transform.forward.y * pitchModSpeedRate * Time.deltaTime;
		// Make sure we never to too fast and that forwardSpeed never goes below zero.
		forwardSpeed = Mathf.Clamp(forwardSpeed, 0, 8f);

		// Move forward as modified by forwardSpeed and speed.
		transform.Translate(transform.forward * speed * forwardSpeed * Time.deltaTime, Space.World);

		// If we ever go too slow, turn on the gravity on the rigid body so that the plane falls.
		if (forwardSpeed <= 0.1f) {
			rb.isKinematic= false;
			rb.useGravity = true;
		}

		// Make sure we never go below the ground. First, we find out what the height of the terrain is at
		// the position the plane is in. If the plane's y position is below that position, we know we have gone
		// too low. In the if statement, we simply place the plane at the height of the terrain.
		// But this is where you could have the plane crash, or have it slow down, or something.
		float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position);
		if (transform.position.y < terrainHeight) {
			transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
		}

		// Shoot an apple pie whenever the player presses space.
		if (Input.GetKeyDown(KeyCode.Space)) {
			GameObject applePie = Instantiate(applePiePrefab, transform.position + transform.forward * 3, Quaternion.identity);
			Rigidbody applePieRB = applePie.GetComponent<Rigidbody>();
			applePieRB.AddForce(transform.forward * 8000);
			Destroy(applePie, 5);
		}
		
    	// Position the camera behind and above the player.
		Vector3 cameraPosition = transform.position - transform.forward * 12 + Vector3.up * 5;
		Camera.main.transform.position = cameraPosition;
		Vector3 lookAtPos = transform.position + transform.forward * 8;
		
		// Rotate the camera so that it looks always in front of the plane.
		Camera.main.transform.LookAt(lookAtPos, Vector3.up);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Ring(Clone)") {
			speed = Mathf.Clamp(speed + 8, 0, maxSpeed);

			score++; 
			GameObject.Find("ScoreText").GetComponent<Text>().text = score.ToString();
		}

		if (other.CompareTag("terrain"))
		{
			PlatformFather.SetActive(true);
			PlatformFather.transform.position = new Vector3(this.gameObject.transform.position.x - player.transform.localPosition.x, this.gameObject.transform.position.y - player.transform.localPosition.y, this.gameObject.transform.position.z - player.transform.localPosition.z);
			player.SetActive(true);
			//player.transform.localPosition = new Vector3(255.22f, 1.746627f, -86.89f);
			//player.AddComponent<PlayerController>();

			Destroy(this.gameObject);
			Destroy(GameObject.Find("Terrain").gameObject);
		}
	}
}
