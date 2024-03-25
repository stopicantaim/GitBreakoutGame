using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {
	GameManager GM;
	void Start () {
		GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	void OnTriggerEnter (Collider col) {
		GM.LoseLife();
	}
}
