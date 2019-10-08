using UnityEngine;
using System.Collections;

public class GunHandler : MonoBehaviour {

	public static GameObject equipedGun;

	void Start () {
		
	}

	void Update () {
		equipedGun.transform.SetParent (this.transform);
	}

	public static void setEquipedGun(GameObject gun) {
		if (equipedGun != null) {
			equipedGun.GetComponent<GunWeapon>().equiped = false;
		}
		gun.GetComponent<GunWeapon> ().equiped = true;
		equipedGun = gun;
	}
}