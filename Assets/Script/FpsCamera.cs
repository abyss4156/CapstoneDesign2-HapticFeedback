using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCamera : MonoBehaviour {

	public GameObject eye;
	public float speed = 5.0f;
	public float sensitivity = 10.0f;
	CharacterController player;

	void Start(){
		player = GetComponent<CharacterController> ();
	}


	void Update(){
		float rotateX = Input.GetAxis ("Mouse X") * sensitivity;
		float rotateY = Input.GetAxis ("Mouse Y") * sensitivity;

		float moveH = Input.GetAxis ("Horizontal")*speed;
		float moveV = Input.GetAxis ("Vertical")*speed;

		Vector3 movement = new Vector3 (moveH, 0, moveV);
		transform.Rotate (0, rotateX, 0);
		eye.transform.Rotate (-rotateY, 0, 0);

		movement = transform.rotation * movement;

		player.Move (movement*Time.deltaTime);
	}
}
