using UnityEngine;
using System.Collections;

public class PlayerRayCast : MonoBehaviour {
	public GameObject player;
	public GameObject inventoryObject;
	public Inventory inventory;

	RaycastHit hit;
	public static bool inAction = false;
	private static bool lockMouse = false;

	private RaycastHit hitObject;
	private bool openDoorEvent = false;
	private bool grabEvent = false;
	private bool vehicleEvent = false;
	private Transform grabPosition;

	void Start () {
		inventory = inventoryObject.GetComponent<Inventory> ();
	}

	void Update () {
		ActionHand.show = false;
		ActionHand.grab = false;
		inAction = false;

		if (Input.GetMouseButtonUp (0)) {
			openDoorEvent = false;
		}
		Vector3 front = transform.forward;

		if (Physics.Raycast(transform.position, front, out hit, 3, ~(1 << 2))) {
			if (hit.collider.tag.Equals("Door")) {
				ActionHand.show = true;
				inAction = true;
				if (Input.GetMouseButtonDown(0)) {
					openDoorEvent = true;
					hitObject = hit;
				}
			}
			if (hit.collider.tag.Equals("Loot")) {
				ActionHand.show = true;
				inAction = true;
				if (Input.GetMouseButton(0)) {
					grabEvent = true;
					hitObject = hit;
				}
			}
			if (hit.collider.tag.Equals("Vehicle")) {
				ActionHand.show = true;
				inAction = true;
				if (Input.GetMouseButton(0)) {
					vehicleEvent = true;
					hitObject = hit;
				}
			}
		}

		if (vehicleEvent) {
			Vehicle veh = hitObject.collider.gameObject.GetComponent<Vehicle>();
			player.GetComponent<CharacterController>().enabled = false;
			player.GetComponent<CapsuleCollider>().enabled = false;
			player.transform.position = veh.playerPosition.transform.position;
			veh.isPlayer = true;

			if (Input.GetKey(KeyCode.Escape)) {
				player.GetComponent<CharacterController>().enabled = true;
				player.GetComponent<CapsuleCollider>().enabled = true;
				veh.isPlayer = false;
				player.transform.SetAsFirstSibling();
				vehicleEvent = false;
			}

			return;
		}

		if (grabEvent) {
			GameObject item = hitObject.transform.gameObject;
			if (inventory.addItem(item.GetComponent<Loot>().getItem()))
				item.gameObject.SetActive(false);

			grabEvent = false;
		}

		if (openDoorEvent) {
			ActionHand.show = true;
			inAction = true;
			lockMouse = true;
			if (Input.GetMouseButton (0)) {
				ActionHand.grab = true;
				openDoorEvent = true;

				float angle = 0;
				float xSpeed = Input.GetAxis("Mouse X");
				float ySpeed = Input.GetAxis("Mouse Y");
				angle = Mathf.Sqrt (xSpeed * xSpeed + ySpeed * ySpeed) * 2f;
				if (Input.GetAxis("Mouse X") < 0) angle = -angle;

				hitObject.transform.Rotate (new Vector3 (0, angle, 0));
			} else {
				openDoorEvent = false;
			}
		} else {
			lockMouse = false;
			inAction = false;
		}
	}


	public static bool isMouseLocked() {
		return lockMouse;
	}
}
