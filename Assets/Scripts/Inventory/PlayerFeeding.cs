using UnityEngine;
using System.Collections;

public class PlayerFeeding : MonoBehaviour {

	public GameObject player;

	void Start () {
	
	}

	void Update () {
		if (this.transform.childCount != 0) {

			GameObject child = this.transform.GetChild(0).gameObject;

			int thirst = child.GetComponent<Item>().thirst;
			int hunger = child.GetComponent<Item>().hunger;

			player.GetComponent<Player>().feed(thirst, hunger);

			for (int i = 0; i < transform.childCount; i++) {
				Destroy (this.transform.GetChild(i).gameObject);
			}
			this.GetComponent<ItemSlot>().isUsed = false;
		}
	}
}
