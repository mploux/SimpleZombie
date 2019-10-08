using UnityEngine;
using System.Collections;

public class ItemSlotGrid : MonoBehaviour {

	public GameObject panel;
	public GameObject slot;
	public int size;

	private ItemSlot[] slots;

	void Start () {
		slots = new ItemSlot[size];

		for (int i = 0; i < size; i++) {
			Instantiate(slots[i], slots[i].transform.position, slots[i].transform.rotation);
			slots[i].transform.SetParent(panel.transform);
			slots[i].id = i;
		}
	}

	void Update () {

	}
}