using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	float directionX = 0;
	Rigidbody2D rb;

//
//	private Vector3 gravity = new Vector3(0, 0.02f, 0);
//	public GameObject micVolume;
//	private float moveSpeed;
//
//
//	void FixedUpdate(){
//		/* Move PLayer upwards based on Mic volume */
//
//		moveSpeed = micVolume.GetComponent<MicrophoneInput>().loudness * 0.01f;
//		transform.position = new Vector3(0, transform.position.y + moveSpeed, 0);
//
//		/* Simulate our own gravity (this one doesn't get stronger when high) */
//		transform.position -= gravity;
//	}
//
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//directionX = CrossPlatformInputManager.GetAxis ("Horizontal");
		Debug.Log("_axis: " + directionX);
		directionX = AudioPeer._axis;
		rb.velocity = new Vector2 (directionX * 10, 0);

	}
}
