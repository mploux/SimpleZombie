using UnityEngine;
using System.Collections;

public class BuldingLootSpawn : MonoBehaviour {

	public GameObject[] loots = new GameObject[10];
	public int[] chances = new int[10];

	public Transform[] spawns = new Transform[10];
	public int lootCount;
	public int spawnCount;
	public int numSpawn;

	void Start () {
		if (numSpawn > spawnCount)
			numSpawn = spawnCount;

		for (int i = 0; i < numSpawn; i++) {
			int loot = Random.Range(0, lootCount);
			if (Random.Range(0, 100) < chances[loot]) {
				Instantiate(loots[loot], spawns[i].position, loots[loot].transform.rotation);
			}
		}
	}
}
