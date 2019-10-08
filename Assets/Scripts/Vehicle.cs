using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {

	Rigidbody body;
	public bool isPlayer = false;
	public Transform playerPosition;
	public bool leave = false;

	void Start () {
		body = GetComponent<Rigidbody> ();
	}

	void Update () {
		if (isPlayer && leave) {
			isPlayer = false;
		}
		if (!isPlayer)
			return;


		float vertical = -Input.GetAxis ("Vertical");

		body.AddForce(transform.forward * vertical * Time.deltaTime * 700);
		transform.Rotate (new Vector3(0, Input.GetAxis ("Horizontal"), 0));


	}
}
