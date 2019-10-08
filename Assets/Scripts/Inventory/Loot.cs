using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour {

	public GameObject itemObject;
	private Item item;
	public GameObject player;

	void Start () {
		item = itemObject.GetComponent<Item> ();
	}


	bool cull = false;
	void Update () {
		float dist = Vector3.Distance(player.transform.position, transform.position);

		if (dist > 10) {
			if (!cull) {
				for (int i = 0; i < this.transform.childCount; i++) {
					this.transform.GetChild (i).gameObject.SetActive (false);
				}
			}
			cull = true;
			return;
		} else {
			if (cull) {
				for (int i = 0; i < this.transform.childCount; i++) {
					this.transform.GetChild (i).gameObject.SetActive (true);
				}
			}
			cull = false;
		}
	}

	public Item getItem() {
		return item;
	}
}
