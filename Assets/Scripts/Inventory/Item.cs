using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public static string FOOD = "FOOD";
	public static string GUN = "GUN";

	public GameObject objectToEquipe;
	public GameObject equipeObject;
	public GameObject lootObject;
	public GameObject itemObject;
	public string type;
	public int thirst = 0;
	public int hunger = 0;
	public ItemSlot slot;

	void Start() {
		itemObject = this.gameObject;
		if (objectToEquipe != null) {
			equipeObject = Instantiate (objectToEquipe, new Vector3 (), new Quaternion ()) as GameObject;
			equipeObject.SetActive (false);
		}


	}
}
