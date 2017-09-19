using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	private GameObject tower;
	private BlockController bc;

	// Use this for initialization
	void Start () {
		transform.gameObject.SetActive (true);
		tower = GameObject.FindGameObjectWithTag ("ground");
		bc = tower.GetComponent<BlockController> ();
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "pb") {
			bc.setGameOver ();
			transform.gameObject.SetActive (false);
		}
	}
}
