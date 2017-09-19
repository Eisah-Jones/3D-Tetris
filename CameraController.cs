using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject[] CameraPoints;
	public float cameraSpeed;
	public GameObject tower;

	private int currentTarget;
	private Vector3 center = new Vector3(0, 6.12f, 0);
	private BlockController bc;

	// Use this for initialization
	void Start () {
		bc = tower.GetComponent<BlockController> ();
		transform.position = new Vector3 (0f, 6.12f, -26f);
		currentTarget = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!bc.gameOver) {
			if (currentTarget < 0) {
				currentTarget = 3;
			}

			if (currentTarget > 3) {
				currentTarget = 0;
			}


			if (transform.position == CameraPoints [currentTarget].transform.position) {
				if (Input.GetKey (KeyCode.LeftArrow)) {
					currentTarget -= 1;
					bc.blockWithCameraPosition (currentTarget + 1, currentTarget);
					bc.blockWithCameraRotation (currentTarget + 1, currentTarget);
				}

				if (Input.GetKey (KeyCode.RightArrow)) {
					currentTarget += 1;
					bc.blockWithCameraPosition (currentTarget - 1, currentTarget);
					bc.blockWithCameraRotation (currentTarget - 1, currentTarget);
				}
			}

			if (currentTarget < 0) {
				currentTarget = 3;
			}

			if (currentTarget > 3) {
				currentTarget = 0;
			}

			if (transform.position != CameraPoints [currentTarget].transform.position) {
				transform.position = Vector3.MoveTowards (transform.position, CameraPoints [currentTarget].transform.position, cameraSpeed * Time.deltaTime);
				transform.LookAt (center);
			}
		}
	}

	public int getCurrentTarget(){
		return currentTarget;
	}
}
