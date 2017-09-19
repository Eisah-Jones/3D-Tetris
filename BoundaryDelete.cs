using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoundaryDelete : MonoBehaviour {

	public static List<GameObject> toDelete;

	// Use this for initialization
	void Start () {
		toDelete = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "pb") {
			toDelete.Add (other.gameObject);
		}
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "pb" && !toDelete.Contains(other.gameObject)) {
			toDelete.Add (other.gameObject);
		}
	}

	public static void deleteList(){
		foreach (GameObject o in toDelete) {
			Destroy (o);
		}
		toDelete = new List<GameObject> ();
	}
}
