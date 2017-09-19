using UnityEngine;
using System.Collections;

public class SmallCubes : MonoBehaviour {

	private GameObject rc;
	private CheckRows cr;

	void Start(){
		rc = GameObject.FindGameObjectWithTag ("check");
		cr = rc.GetComponent<CheckRows> ();
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "check") {
			cr.addList (transform.gameObject);
		}
	}
}
