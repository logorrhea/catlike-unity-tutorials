using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

	[Range(5, 100)]
	public float acceleration;
	
	[Range(5,100)]
	public float jumpForce;
	
	public static float distanceTraveled = 0;
	private bool touchingPlatform;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Jump") && touchingPlatform) {
			rigidbody.AddForce(1f, jumpForce, 0f, ForceMode.VelocityChange);
			touchingPlatform = false;
		}
		distanceTraveled = transform.position.x;
	}
	
	void FixedUpdate() {
		if (touchingPlatform) {
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}
	
	void OnCollisionEnter() {
		touchingPlatform = true;
	}
	
	void OnCollisionExit() {
		touchingPlatform = false;
	}
}
