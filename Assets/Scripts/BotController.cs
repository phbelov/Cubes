using UnityEngine;
using System.Collections;

public class BotController : MonoBehaviour {

	public GameObject character;	
	public GameObject botBullet;

	public float speed = 10.0f;

	public float directionPower = 10.0f;
	public float destroyTime;

	public Vector3 randomVector;

	private int health = 100;

	private Vector3 initialScale;

	void Start () {
		initialScale = character.transform.localScale;
	}

	void Update () {
		{
			Vector3 targetPosition = character.transform.position + randomVector;

			var deltaPosition = -gameObject.transform.position + targetPosition;
			var targetVelocity = deltaPosition.normalized * speed;
	
			if (deltaPosition.magnitude > 0.5f) {
				var velocity = gameObject.GetComponent<Rigidbody> ().velocity;

				gameObject.GetComponent<Rigidbody> ().AddForce (targetVelocity - velocity, ForceMode.VelocityChange);		
			}
		}

		{
			var deltaPosition = -gameObject.transform.position + character.transform.position;
			if (deltaPosition.magnitude > 1.0 && Random.Range (1, 300) == 5) {
				var direction = deltaPosition.normalized;
				SpawnBotBullet (gameObject.transform.position, direction);
			}
		}

		if (health <= 0) { 
			Destroy (gameObject);
//			GameObject.Find ("Bot Spawner").GetComponent<BotSpawner>().SpawnNewBot();
		}
	}

	void OnCollisionEnter(Collision theCollision) {
		if (theCollision.gameObject.CompareTag("Bullet")) {
			Debug.Log ("hello");
			health -= 20;
			gameObject.transform.localScale *= 0.8f;
		}
	}

	void SpawnBotBullet (Vector3 pos, Vector3 direction) {
		var newBullet = (GameObject) Instantiate(botBullet, pos + direction * 2, Quaternion.identity);
		newBullet.transform.localScale = character.transform.localScale;
		newBullet.GetComponent<Rigidbody> ().AddForce (direction * directionPower, ForceMode.Impulse);
		StartCoroutine (BulletDestroyer (newBullet));
	}

	IEnumerator BulletDestroyer (GameObject aBullet) {
		yield return new WaitForSeconds (destroyTime);
		Destroy (aBullet);
	}

}
