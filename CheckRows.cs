using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckRows : MonoBehaviour {

	private List<GameObject> pieceList;
	private int rowsDeleted;
	private Scoring sc;

	// Use this for initialization
	void Start () {
		sc = Camera.main.GetComponent<Scoring> ();
		pieceList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addList(GameObject o){
		pieceList.Add (o);
	}

	public void check(){
		//Check row to see if there are 20 blocks
		//Remove those blocks
		//Trigger all above blocks to fall
		//Continue with turn
		StartCoroutine (runCheck());

	}

	private IEnumerator runCheck(){
		rowsDeleted = 0;
		yield return new WaitForSeconds (0.1f);
		for (int i = 0; i < 10; i++) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
			yield return new WaitForSeconds (0.1f);
			if (pieceList.Count >= 20) {
				rowsDeleted += 1;
				foreach (GameObject c in pieceList) {
					Destroy (c);
				}
				sc.clearRow ();
			} else {
				foreach (GameObject c in pieceList) {
					try{
						c.transform.position = new Vector3 (c.transform.position.x, c.transform.position.y - rowsDeleted, c.transform.position.z);
					} catch {
						//Do Nothing
					}
				}
			}
			pieceList = new List<GameObject> ();
		}

		transform.position = new Vector3 (0, 0.5f, 0);
	}
}
