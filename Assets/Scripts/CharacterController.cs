using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float cubeVelocity = 0.5f;
	public float directionPower = 100.0f;
	public float fireCooldown = 0.001f;
	public float rotationSpeed = 7.5f;
	public float destroyTime = 1.0f;
	public GameObject bullet;

	private float lastFire;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.forward * cubeVelocity);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (Vector3.back * cubeVelocity);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * cubeVelocity);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * cubeVelocity);
		}

		if (Input.GetKey (KeyCode.Q)) {
			transform.RotateAround (Vector3.up, Time.deltaTime * rotationSpeed);
		}
		if (Input.GetKey (KeyCode.E)) {
			transform.RotateAround (Vector3.up, -Time.deltaTime * rotationSpeed);
		}
			
		if (Input.GetKey (KeyCode.Space) && Time.time - lastFire > fireCooldown) {
			SpawnBullet (gameObject.transform.position, gameObject.transform.forward);			
			SpawnBullet (gameObject.transform.position, Quaternion.AngleAxis(90, Vector3.up) * gameObject.transform.forward);
			SpawnBullet (gameObject.transform.position, Quaternion.AngleAxis(180, Vector3.up) * gameObject.transform.forward);
			SpawnBullet (gameObject.transform.position, Quaternion.AngleAxis(270, Vector3.up) * gameObject.transform.forward);
			lastFire = Time.time;
		}
			
	}

	void OnCollisionEnter() {
		if (transform.localScale.magnitude > 0.25) {
			float dY = -transform.position.y * 0.1f;
			transform.localScale *= 0.9f;
			transform.Translate (0, dY, 0);
		} 
	}

	void SpawnBullet (Vector3 pos, Vector3 direction) {
		var newBullet = (GameObject) Instantiate(bullet, pos + direction * 2, Quaternion.identity);
		newBullet.GetComponent<Rigidbody> ().AddForce (direction * directionPower, ForceMode.Impulse);
		StartCoroutine (BulletDestroyer (newBullet));
	}

	IEnumerator BulletDestroyer (GameObject aBullet) {
		yield return new WaitForSeconds (destroyTime);
		Destroy (aBullet);
	}
}