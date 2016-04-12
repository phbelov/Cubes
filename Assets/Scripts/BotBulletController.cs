using UnityEngine;
using System.Collections;

public class BotBulletController : MonoBehaviour {
	
	void OnCollisionEnter() {
		Destroy (gameObject);
	}
}
