using UnityEngine;
using System.Collections;

public class MouseLock : MonoBehaviour {

	private static bool grabbed = false;

	void Start () {
	
	}

	void Update () {
		if (Input.GetMouseButton (0) && !grabbed) {
			setLocked(true);
		}
		if (Input.GetKey (KeyCode.Escape) && grabbed) {
			setLocked(false);
		}
		if (Inventory.opened)
			setLocked(false);

		if (grabbed) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		//print ("Mouse Locked: " + grabbed);
	}

	public static bool isLocked() {
		return grabbed;
	}

	public static void setLocked(bool locked) {
		grabbed = locked;
	}
}
