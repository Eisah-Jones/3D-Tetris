using UnityEngine;
using System.Collections;

public class MainMenuCamera : MonoBehaviour {

	private Vector3 howToPos;
	private Vector3 startPos;

	private bool howToClicked;
	private bool backClicked;

	// Use this for initialization
	void Start () {
		startPos = new Vector3 (0, -0.9f, -17.2f);
		howToPos = new Vector3 (-10.93f, -1.55f, -17.24f);
		transform.position = startPos;
		howToClicked = false;
		backClicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (howToClicked) {
			transform.position = Vector3.MoveTowards (transform.position, howToPos, 10f * Time.deltaTime);
			if (transform.position == howToPos) {
				howToClicked = false;
			}
		}

		if (backClicked) {
			transform.position = Vector3.MoveTowards (transform.position, startPos, 10f * Time.deltaTime);
			if (transform.position == startPos) {
				howToClicked = false;
			}
		}
	}

	public void moveTo(){
		howToClicked = true;
		backClicked = false;
	}

	public void moveBack(){
		backClicked = true;
		howToClicked = false;
	}
}
