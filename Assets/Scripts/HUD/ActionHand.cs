using UnityEngine;
using System.Collections;

public class ActionHand : MonoBehaviour {

	public static bool show = false;

	public GameObject openHand;
	public GameObject grabHand;
	public static bool grab = false;

	void Start () {
	
	}

	void Update () {
		openHand.SetActive(!grab && show);
		grabHand.SetActive(grab && show);
	}
}