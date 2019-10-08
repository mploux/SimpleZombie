using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public GameObject player;
	public GameObject deathScreen;

	public GameObject gunHandler;
	public Transform equipeBox;

	public GameObject inventory;
	public GameObject slot;
	public Transform dropPosition;
	public Transform dropBox;
	public int size;

	private List<ItemSlot> slots;
	public int usedSlots;
	private List<GameObject> slotsObject;
	private int itemsSize;

	private Transform equipedObjectParent;
	private GameObject equipedObject;

	public static bool opened = false;

	void Start () {
		inventory.SetActive(false);

		slots = new List<ItemSlot>();
		usedSlots = 0;
		slotsObject = new List<GameObject> ();

		for (int i = 0; i < size; i++) {
			slots.Add(new ItemSlot(i));
			slots[i] = slot.GetComponent<ItemSlot>();
			GameObject instancedSlot = Instantiate(slot, slots[i].transform.position, slots[i].transform.rotation) as GameObject;
			instancedSlot.transform.SetParent(inventory.transform.Find("BackPackPanel").transform);
			slotsObject.Add(instancedSlot);
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			opened = !opened;
		}

		if (opened) {
			inventory.SetActive(true);
		} else {
			inventory.SetActive(false);
		}

		if (dropBox.childCount != 0) {
			int childs = dropBox.childCount;

			for (int i = 0; i < childs; i++) {
				dropItem(dropBox.GetChild(i).gameObject);
				usedSlots--;
			}
		}

		if (equipeBox.childCount != 0) {
			if (equipedObject == null) {
				equipedObject = equipeBox.GetChild (0).gameObject;
				equipedObjectParent = equipedObject.transform;
				print ("LOL: " + equipedObject.GetComponent<Item> ().name);

				equipedObject.GetComponent<Item> ().equipeObject.transform.SetParent (gunHandler.transform);
				equipedObject.GetComponent<Item> ().equipeObject.SetActive (true);
				equipedObject.GetComponent<Item> ().equipeObject.transform.localPosition = new Vector3(0, 0, 0);
				equipedObject.GetComponent<Item> ().equipeObject.transform.localRotation = new Quaternion(0, 0, 0, 1);
				equipedObject.GetComponent<Item> ().equipeObject.transform.localScale = new Vector3(1, 1, 1);

				usedSlots--;
			}
		} else {
			if (equipedObject != null) {
				equipedObject.GetComponent<Item> ().equipeObject.transform.SetParent(equipedObjectParent);
				equipedObject.GetComponent<Item> ().equipeObject.SetActive (false);
				equipedObject = null;
			}
		}

		if (Input.GetKeyDown (KeyCode.E) && !opened) {
			MouseLock.setLocked(true);
		}

		if (player.GetComponent<Player> ().dead) {
			deathScreen.SetActive (true);
			for (int i = 0; i < slotsObject.Count; i++) {
				if (slotsObject [i].transform.childCount != 0) {
					Destroy (slotsObject [i].transform.GetChild (0).gameObject);
				}
			}
			usedSlots = 0;

			if (equipeBox.childCount != 0) {
				Destroy (equipeBox.GetChild (0).gameObject);
				Destroy (gunHandler.transform.GetChild (0).gameObject);
				equipeBox.GetComponent<ItemSlot>().isUsed = false;
			}
		} else {
			deathScreen.SetActive(false);
		}
	}

	public bool addItem(Item item) {
		if (usedSlots - 1 >= slots.Count)
			return false;

		item.itemObject.transform.position = new Vector3 (0, 0, 0);
		item.itemObject.transform.rotation = new Quaternion (0, 0, 0, 1);

		GameObject instancedItem = Instantiate(item.itemObject, item.itemObject.transform.position, item.itemObject.transform.rotation) as GameObject;
		instancedItem.transform.SetParent(slotsObject[usedSlots].transform);
		usedSlots++;

		return true;
	}

	public void dropItem(GameObject item) {
		item.GetComponent<Item> ().lootObject.SetActive (true);
		item.GetComponent<Item> ().lootObject.transform.position = dropPosition.position;
		item.GetComponent<Item> ().lootObject.transform.rotation = dropPosition.rotation;
		item.GetComponent<Item> ().lootObject.GetComponent<Rigidbody>().AddForce (dropPosition.forward * 100);
		Destroy (item);
		dropBox.GetComponent<ItemSlot> ().isUsed = false;
	}
}