using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour {

	public GameObject player;
	private Animator anim;

	private CharacterController controller;

	private Vector2 dir;
	private Vector3 moveDir;
	private float speed = 0.5f;
	public float randomRotation;
	public float rotation;
	public bool agroPlayer;
	public bool attackPlayer;
	public bool dead;
	public bool die;
	private int life = 100;

	public bool startAnimation;
	public int startAnimationTime;

	void Start () {
		dir = new Vector2 (0, 1);
		moveDir = new Vector2 (0, 0);
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();

		startAnimationTime = Random.Range (0, 120);
	}

	void Update() {

	}

	int time = 0;
	int attackTime = 0;

	bool cull = false;
	void FixedUpdate() {
		float dist = Vector3.Distance (player.transform.position, transform.position);


		if (dist > 100) {
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

		if (dead)
			return;

		time++;

		if (time > startAnimationTime)
			startAnimation = true;

		if (!startAnimation)
			return;

		if (attackPlayer)
			attackTime++;
		else
			attackTime = 0;

		if (attackPlayer) {
			anim.SetBool ("Attack", true);
			anim.SetBool ("Walk", false);

			if (attackTime % 60 == 0) {
				player.GetComponent<Player>().addDamage(20);
			}

		} else if (die) {
			dead = true;
			controller.enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			anim.SetBool ("Walk", false);
			anim.SetBool ("Attack", false);
			anim.CrossFade("back_fall", 0f);

			return;
		} else {
			anim.SetBool ("Walk", true);
			anim.SetBool ("Attack", false);
		}
 		if (agroPlayer) {
			anim.SetFloat("Speed", 1.5f);
			speed = 0.75f;

			Vector3 playerPos = new Vector3(
				player.transform.position.x, 
				controller.transform.position.y, 
				player.transform.position.z
			);

			Quaternion startLook = controller.transform.rotation;
			controller.transform.LookAt(playerPos);
			Quaternion look = controller.transform.rotation;

			controller.transform.rotation = Quaternion.Lerp(startLook, look, Time.deltaTime * 3f);
		} else {
			anim.SetFloat("Speed", 1f);
			speed = 0.5f;
			randomRotation += (Random.value * 2 - 1) * 0.5f;
		}

		// always move along the camera forward as it is the direction that it being aimed at
		Vector3 desiredMove = transform.forward*dir.y + transform.right*dir.x;
		
		// get a normal for the surface that is being touched to move along it
		RaycastHit hitInfo;
		Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hitInfo,
		                   controller.height/2f);
		desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

		moveDir.z = desiredMove.z*speed;
		moveDir.x = desiredMove.x*speed;
		
		if (controller.isGrounded)
		{
			moveDir.y = -10;
		}
		else
		{
			moveDir += Physics.gravity*2*Time.fixedDeltaTime;
		}

		controller.Move(moveDir*Time.fixedDeltaTime);
		controller.transform.Rotate(new Vector3(0, randomRotation, 0));


		randomRotation *= 0.9f;
	}

	public void addDamage(int damage) {
		life -= damage;
		if (life <= 0)
			die = true;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			agroPlayer = true;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			float dist = Vector3.Magnitude(other.transform.position - transform.position);
			if (dist < 2.5f) {
				attackPlayer = true;
			}else {
				attackPlayer = false;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			agroPlayer = false;
			attackPlayer = false;
		}
	}
 }
