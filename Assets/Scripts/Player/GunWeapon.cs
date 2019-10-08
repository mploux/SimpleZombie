using UnityEngine;
using System.Collections;

public class GunWeapon : MonoBehaviour {

	public GameObject gun;
	public GameObject smokeParticle;
	public Transform idlePosition;
	public Transform scopePosition;
	public Transform positionState;
	public Transform shootPosition;
	public float shootKickBack;
	public bool equiped = false;

	public bool shoot = false;

	private Vector3 sway;
	private float swayFactor;
	private Camera camera;
	private float fovState;

	void Start () {
		sway = new Vector3 ();
		camera = this.transform.parent.parent.GetComponent<Camera> ();
	}

	void Update () {
		if (Inventory.opened)
			return;

		positionState.localPosition = idlePosition.localPosition;
		swayFactor = 0.25f;
		fovState = 70.0f;

		if (Input.GetMouseButton(1)) {
			swayFactor = 0.05f;
			fovState = 50.0f;
			positionState.localPosition = scopePosition.localPosition;
		}

		if (Input.GetMouseButtonDown (0) && !ActionHand.show) {
			shoot = true;
		}

		if (shoot) {
			positionState.localPosition = new Vector3(positionState.localPosition.x, 
			                                          positionState.localPosition.y, 
			                                          positionState.localPosition.z - shootKickBack);	


			Instantiate(smokeParticle, shootPosition.position, shootPosition.rotation);
			RaycastHit[] hits;
			hits = Physics.RaycastAll(shootPosition.position, shootPosition.forward, Mathf.Infinity, ~(1 << 2));
			
			for (int i = 0; i < hits.Length; i++) {
				if (hits[i].collider.isTrigger || !hits[i].collider.tag.Equals("Zombie")) 
					continue;

				hits[i].transform.gameObject.GetComponent<ZombieMovement>().addDamage(30);
				break;
			}

			shoot = false;
		}

		this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, positionState.localPosition, Time.deltaTime * 20);
		camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, fovState, Time.deltaTime * 20);

		sway.y += Input.GetAxis ("Mouse X") * swayFactor;
		sway.x -= Input.GetAxis ("Mouse Y") * swayFactor;

		this.transform.localRotation = Quaternion.Euler(sway);

		sway.x *= 0.7f;
		sway.y *= 0.7f;
	}
}