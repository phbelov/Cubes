using UnityEngine;
using System.Collections;

public class BotSpawner : MonoBehaviour {

	public GameObject character;
	public GameObject cubeBot;
	public int botNumber;
	public int maxBotNumber = 5;
	public int minBotNumber = 1;

	public GameObject plane;

	void Start() {
		for (var i = 0; i < botNumber; i++) {
			SpawnNewBot ();
		}
	}

	public void SpawnNewBot() {
		var z = Random.Range (plane.GetComponent<Collider>().bounds.min.z, plane.GetComponent<Collider>().bounds.max.z);
		var x = Random.Range (plane.GetComponent<Collider> ().bounds.min.x, plane.GetComponent<Collider> ().bounds.max.x);
		var newCubeBot = (GameObject) Instantiate (cubeBot, new Vector3 (x, 0.5f, z), Quaternion.identity);
		newCubeBot.transform.localScale = Random.Range (0.25f, 2.5f) * character.transform.localScale;
		Vector3 randomVector = new Vector3 (Random.Range(-5.0f,5.0f), 0, Random.Range(-5.0f,5.0f));
		newCubeBot.GetComponent<BotController> ().randomVector = randomVector;
		newCubeBot.GetComponent<BotController> ().character = character;
	}
}
