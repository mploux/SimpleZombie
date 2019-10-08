using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Transform spawn;

	public GameObject healthGui;
	public GameObject hungerGui;
	public GameObject thirstGui;
	public GameObject inventory;

	public int life = 100;
	public int stamina = 100;
	public int hunger = 100;
	public int thirst = 100;

	private int lifeLoss = 0;
	private int hungerLoss = 1;
	private int thirstLoss = 3;

	public bool dead = false;

	void Start () {
		
	}

	int lifeTime = 0;
	int hungerTime = 0;
	int thirstTime = 0;

	float lifeSlowSpeed = 2;
	float hungerSlowSpeed = 5;
	float thirstSlowSpeed = 5;

	int deadTime = 0;
	int deadStopTime = 2;
	void Update () {
		if (dead) {
			deadTime++;

			if (deadTime > deadStopTime * 60) {
				dead = false;
				life = 100;
				thirst = 100;
				hunger = 100;
				lifeLoss = 0;
				hungerLoss = 1;
				thirstLoss = 3;
				deadTime = 0;
			}else if (deadTime > deadStopTime * 60 / 2) {
				transform.position = spawn.position;
				transform.rotation = spawn.rotation;
				transform.localScale = new Vector3(1, 1, 1);
			}
		}

		healthGui.GetComponent<Text> ().text = "Health: " + life;
		hungerGui.GetComponent<Text> ().text = "Hunger: " + hunger;
		thirstGui.GetComponent<Text> ().text = "Thirst: " + thirst;

		lifeTime += lifeLoss;
		hungerTime += hungerLoss;
		thirstTime += thirstLoss;

		if (lifeLoss > 0) {
			if (lifeTime % (60 * lifeSlowSpeed) == 0) {
				life -= 1;
			}
		}
		if (hungerTime % (60 * hungerSlowSpeed) == 0) {
			hunger -= 1;
		}			
		if (thirstTime % (60 * thirstSlowSpeed) == 0) {
			thirst -= 1;
		}

		if (hunger <= 0 || thirst <= 0) {
			lifeLoss = 1;
		}
		if (hunger <= 0 && thirst <= 0) {
			lifeLoss = 2;
		}

		hungerGui.GetComponent<Text> ().color = Color.white;
		thirstGui.GetComponent<Text> ().color = Color.white;
		healthGui.GetComponent<Text> ().color = Color.white;

		if (hunger < 50) {
			hungerGui.GetComponent<Text> ().color = Color.yellow;
		}
		if (thirst < 50) {
			thirstGui.GetComponent<Text> ().color = Color.yellow;
		}
		if (life < 50) {
			healthGui.GetComponent<Text> ().color = Color.yellow;
		}

		if (hunger < 20) {
			hungerGui.GetComponent<Text> ().color = Color.red;
		}
		if (thirst < 20) {
			thirstGui.GetComponent<Text> ().color = Color.red;
		}
		if (life < 20) {
			healthGui.GetComponent<Text> ().color = Color.red;
		}

		if (hunger <= 0) {
			hungerGui.GetComponent<Text> ().color = Color.black;
		}
		if (thirst <= 0) {
			thirstGui.GetComponent<Text> ().color = Color.black;
		}
		if (life <= 0) {
			healthGui.GetComponent<Text> ().color = Color.black;
		}

		if (hunger < 0) {
			hunger = 0;
		} 
		if (thirst < 0) {
			thirst = 0;
		} 
		if (life < 0) {
			life = 0;
			dead = true;
		}
	}

	public void addDamage(int damage) {
		life -= damage;

		if (life <= 0) {
			dead = true;
		}
	}

	public void feed(int thirst, int hunger) {
		inventory.GetComponent<Inventory>().usedSlots--;
		this.thirst += thirst;
		this.hunger += hunger;

		if (this.thirst > 100)
			this.thirst = 100;
		if (this.hunger > 100)
			this.hunger = 100; 
	}
}