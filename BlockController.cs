using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BlockController : MonoBehaviour {

	public GameObject blockI;
	public GameObject blockT;
	public GameObject blockZ;
	public GameObject blockS;
	public GameObject blockL;
	public GameObject blockJ;
	public GameObject blockO;
	public GameObject rc;

	public bool moveRight;
	public bool moveLeft;
	public bool blockFall;
	public bool blockPlaced;
	public bool canRotate;

	public float fallSpeed = 0.3f;

	public Image nextBlockImage;

	public Text pauseText;

	public Button pause;
	public Button menu;
	public Button restart;
	public Button continueGame;


	private CameraController cc;
	private Scoring sc;
	private Action[] blockArray;
	private GameObject currentBlock;
	private Quaternion defaultRot = new Quaternion(0f, 0f, 0f, 0f);
	private int nextBlock = -1;
	public bool doCheckDown = true;
	private string nameT;
	private CheckRows cr;
	public bool gameOver = false;
	private NextBlock nb;
	private GameObject[] allBlocks;

	// Use this for initialization
	void Start () {
		pauseText.gameObject.SetActive (false);
		menu.gameObject.SetActive (false);
		restart.gameObject.SetActive (false);
		continueGame.gameObject.SetActive (false);
		nextBlockImage.gameObject.SetActive(true);

		pause.onClick.AddListener(pauseGame);
		menu.onClick.AddListener(mainMenu);
		restart.onClick.AddListener(restartGame);
		continueGame.onClick.AddListener(continueClick);

		cc = Camera.main.GetComponent<CameraController> ();
		cr = rc.GetComponent<CheckRows> ();
		sc = Camera.main.GetComponent<Scoring> ();
		nb = nextBlockImage.GetComponent<NextBlock> ();

		blockPlaced = false;
		canRotate = true;

		blockArray = new Action[7];
		blockArray[0] = spawnBlockI;
		blockArray[1] = spawnBlockT;
		blockArray[2] = spawnBlockZ;
		blockArray[3] = spawnBlockS;
		blockArray[4] = spawnBlockL;
		blockArray[5] = spawnBlockJ;
		blockArray[6] = spawnBlockO;

		spawnBlock ();
		StartCoroutine (moveDown());
		nameT = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			if (blockPlaced) {
				StartCoroutine(spawnDelay());
				changeBlockPlaced ();
			}

			if (doCheckDown) {
				checkDown ();
			}
			checkCanRotate ();
			checkLeft ();
			checkRight ();
			blockMovement ();
		}
	}

	private IEnumerator spawnDelay(){
		yield return new WaitForSeconds (0.2f);
		spawnBlock (nextBlock);
	}
		
	private void blockMovement(){
		if (!gameOver) {
			if (cc.getCurrentTarget () == 0) {
				if (Input.GetKeyDown (KeyCode.A) && moveLeft) {
					currentBlock.transform.position += Vector3.left;
				}

				if (Input.GetKeyDown (KeyCode.D) && moveRight) {
					currentBlock.transform.position += Vector3.right;
				}

				if (Input.GetKeyDown (KeyCode.W)) {
					currentBlock.transform.Rotate (0f, 0f, 90f);
				}

				if (Input.GetKeyDown (KeyCode.S)) {
					currentBlock.transform.Rotate (0f, 0f, -90f);
				}
			} else if (cc.getCurrentTarget () == 2) {
				if (Input.GetKeyDown (KeyCode.A) && moveLeft) {
					currentBlock.transform.position += Vector3.right;
				}

				if (Input.GetKeyDown (KeyCode.D) && moveRight) {
					currentBlock.transform.position += Vector3.left;
				}

				if (Input.GetKeyDown (KeyCode.W)) {
					currentBlock.transform.Rotate (0f, 0f, 90f);
				}

				if (Input.GetKeyDown (KeyCode.S)) {
					currentBlock.transform.Rotate (0f, 0f, -90f);
				}
			} else if (cc.getCurrentTarget () == 1) {
				if (Input.GetKeyDown (KeyCode.A) && moveLeft) {
					currentBlock.transform.position += Vector3.back;
				}

				if (Input.GetKeyDown (KeyCode.D) && moveRight) {
					currentBlock.transform.position += Vector3.forward;
				}

				if (Input.GetKeyDown (KeyCode.W)) {
					currentBlock.transform.Rotate (0f, 0f, 90f);
				}

				if (Input.GetKeyDown (KeyCode.S)) {
					currentBlock.transform.Rotate (0f, 0f, -90f);
				}
			} else {
				if (Input.GetKeyDown (KeyCode.A) && moveLeft) {
					currentBlock.transform.position += Vector3.forward;
				}

				if (Input.GetKeyDown (KeyCode.D) && moveRight) {
					currentBlock.transform.position += Vector3.back;
				}

				if (Input.GetKeyDown (KeyCode.W)) {
					currentBlock.transform.Rotate (0f, 0f, 90f);
				}

				if (Input.GetKeyDown (KeyCode.S)) {
					currentBlock.transform.Rotate (0f, 0f, -90f);
				}
			}
		}
	}

	public void blockWithCameraPosition(int oldLoc, int newLoc){
		float x = currentBlock.transform.position.x;
		float y = currentBlock.transform.position.y;
		float z = currentBlock.transform.position.z;
		if (oldLoc < newLoc) { //Moved to the right
			if (oldLoc == 0) {
				currentBlock.transform.position = new Vector3 (-1 * z, y, x);
			} else if (oldLoc == 1) {
				currentBlock.transform.position = new Vector3 (-1 * z, y, x);
			} else if (oldLoc == 2) {
				currentBlock.transform.position = new Vector3 (-1 * z, y, x);
			} else {
				currentBlock.transform.position = new Vector3 (-1 * z, y, x);
			}
		} else { //Moved to the left
			if (oldLoc == 0) {
				currentBlock.transform.position = new Vector3 (z, y, -1 * x);
			} else if (oldLoc == 1) {
				currentBlock.transform.position = new Vector3 (z, y, -1 * x);
			} else if (oldLoc == 2) {
				currentBlock.transform.position = new Vector3 (z, y, -1 * x);
			} else {
				currentBlock.transform.position = new Vector3 (z, y, -1 * x);
			}
		}
	}

	public void blockWithCameraRotation(int oldLoc, int newLoc){
		if (oldLoc < newLoc) { //Moved to the right
			if (oldLoc == 0) {
				currentBlock.transform.Rotate(0, 270, 0, 0);
			} else if (oldLoc == 1) {
				currentBlock.transform.Rotate(0, -90, 0, 0);
			} else if (oldLoc == 2) {
				currentBlock.transform.Rotate(0, -90, 0, 0);
			} else {
				currentBlock.transform.Rotate(0, -90, 0, 0);
			}
		} else { //Moved to the left
			if (oldLoc == 0) {
				currentBlock.transform.Rotate(0, 90, 0, 0);
			} else if (oldLoc == 1) {
				currentBlock.transform.Rotate(0, 90, 0, 0);
			} else if (oldLoc == 2) {
				currentBlock.transform.Rotate(0, 90, 0, 0);
			} else {
				currentBlock.transform.Rotate(0, 90, 0, 0);
			}
		}
	}

	private void spawnBlock(int block = -1){
		if (!gameOver) {
			moveRight = true;
			moveLeft = true;
			blockFall = true;
			doCheckDown = true;
			if (block == -1) {
				System.Random randomNumber = new System.Random ();
				int chooseBlock = randomNumber.Next (0, 7);
				blockArray [chooseBlock] ();
				nextBlock = randomNumber.Next (0, 7);
				nb.changeSprite (nextBlock);
			} else {
				System.Random randomNumber = new System.Random ();
				blockArray [block] ();
				nextBlock = randomNumber.Next (0, 7);
				nb.changeSprite (nextBlock);
			}
		}
	}

	private void spawnBlockI(){
		Vector3 pos = calcPosition ();
		pos += new Vector3(0f, 13.5f, 0f);
		currentBlock = Instantiate(blockI, pos, defaultRot) as GameObject;
		calcRotation ();
	}

	private void spawnBlockT(){
		Vector3 pos = calcPosition ();
		pos += new Vector3(0f, 13.5f, 0f);
		currentBlock = Instantiate(blockT, pos, defaultRot) as GameObject;
		calcRotation ();
	}

	private void spawnBlockZ(){
		Vector3 pos = calcPosition ();
		pos += new Vector3(0f, 13.5f, 0f);
		currentBlock = Instantiate(blockZ, pos, defaultRot) as GameObject;
		calcRotation ();
	}

	private void spawnBlockL(){
		Vector3 pos = calcPosition ();
		currentBlock = Instantiate(blockL, pos, defaultRot) as GameObject;
		calcRotation ();
		if (currentBlock.transform.rotation.y == 0f) {
			pos += new Vector3 (0f, 13.5f, 0f);
		} else {
			pos += new Vector3 (0f, 13.5f, 0f);
		}
		currentBlock.transform.position = pos;
	}

	private void spawnBlockJ(){
		Vector3 pos = calcPosition ();
		currentBlock = Instantiate(blockJ, pos, defaultRot) as GameObject;
		calcRotation ();
		if (currentBlock.transform.rotation.y == 0f) {
			pos += new Vector3 (-0f, 13.5f, 0f);
		} else {
			pos += new Vector3 (0f, 13.5f, 0f);
		}
		currentBlock.transform.position = pos;
	}

	private void spawnBlockS(){
		Vector3 pos = calcPosition ();
		pos += new Vector3(0f, 13.5f, 0f);
		currentBlock = Instantiate(blockS, pos, defaultRot) as GameObject;
		calcRotation ();
	}

	private void spawnBlockO(){
		Vector3 pos = calcPosition ();
		currentBlock = Instantiate(blockO, pos, defaultRot) as GameObject;
		calcRotation ();
		if (currentBlock.transform.rotation.y == 0f) {
			pos += new Vector3 (0f, 13.5f, 0f);
		} else {
			pos += new Vector3 (0f, 13.5f, 0f);
		}
		currentBlock.transform.position = pos;
	}

	private Vector3 calcPosition(){
		if (cc.getCurrentTarget () == 0) {
			return new Vector3 (0f, 0f, -3f);
		} else if (cc.getCurrentTarget () == 1) {
			return new Vector3 (3f, 0f, 0f);
		} else if (cc.getCurrentTarget () == 2) {
			return new Vector3 (0f, 0f, 3f);
		} else {
			return new Vector3 (-3f, 0f, 0f);
		}
	}

	private void calcRotation(){
		if (cc.getCurrentTarget () == 1 || cc.getCurrentTarget () == 3) {
			currentBlock.transform.Rotate (0f, 90f, 0f);
		}
	}

	private IEnumerator moveDown(){
		while (!gameOver) {
			if (!blockPlaced && blockFall) {
				yield return new WaitForSeconds (fallSpeed);
				currentBlock.transform.position += Vector3.down;
				checkDown ();
			}
			yield return new WaitForSeconds (0.5f);
		}
	}

	public void changeBlockPlaced(){
		blockPlaced = !blockPlaced;
	}

	public void changeLeft(){
		moveLeft = !moveLeft;
	}

	public void changeRight(){
		moveRight = !moveRight;
	}

	public void changeFall(){
		blockFall = !blockFall;
		StartCoroutine (stallPlay ());
	}

	private IEnumerator stallPlay(){
		bool sameHit = false;
		yield return new WaitForSeconds (1);
		Transform[] children = currentBlock.GetComponentsInChildren<Transform> ();
		foreach (Transform child in children){
			Debug.DrawRay(child.position, Vector3.down*1, Color.green);
			RaycastHit hit;
			Debug.DrawRay(child.position, Vector3.down, Color.green);
			if (Physics.Raycast (child.position, Vector3.down, out hit, 1f)) {
				if(hit.collider.gameObject.name == nameT){
					blockPlaced = true;
					sameHit = true;
					checkOverlap ();
					activateChildren ();
					cr.check ();
					sc.placeBlock ();
					StartCoroutine (outOfBounds());
					break;
				}
			}

		}

		if (!sameHit) {
			blockFall = true;
			doCheckDown = true;
		}

	}

	private void checkOverlap(){
		allBlocks = GameObject.FindGameObjectsWithTag("pb");
		Transform[] children = currentBlock.GetComponentsInChildren<Transform> ();
		foreach (Transform child in children){
			foreach(GameObject o in allBlocks){
				if (Vector3.Distance(child.position, o.transform.position) <= 0.75f){
					Destroy(child.gameObject);
				}
			}
		}
	}

	private IEnumerator outOfBounds(){
		yield return new WaitForSeconds (0.075f);
		BoundaryDelete.deleteList ();
	}

	public void checkCanRotate(){
		//If not touching either of the sides, any blocks, or the base, you can rotate
		if(moveRight && moveLeft && blockFall){
			canRotate = true;
		} else{
			canRotate = false;
		}
	}

	public void checkLeft(){
		Vector3 checkDirection;
		bool moveBack = true;
		if (cc.getCurrentTarget() == 0) {
			checkDirection = Vector3.left;
		} else if (cc.getCurrentTarget() == 1) {
			checkDirection = Vector3.back;
		} else if (cc.getCurrentTarget() == 2) {
			checkDirection = Vector3.right;
		} else {
			checkDirection = Vector3.forward;
		}


		Transform[] children = currentBlock.GetComponentsInChildren<Transform> ();
		foreach (Transform child in children){
			RaycastHit hit;
			Debug.DrawRay(child.position, checkDirection*1, Color.green);
			if (Physics.Raycast (child.position, checkDirection, out hit, 1)) {
				if(hit.collider.tag == "pb"){
					moveLeft = false;
					moveBack = false;
					break;
				}

				if (hit.collider.tag == "1") {
					if (cc.getCurrentTarget() == 0) {
						moveLeft = false;
						moveBack = false;
						break;
					}
				}

				if (hit.collider.tag == "2") {
					if (cc.getCurrentTarget() == 1) {
						moveLeft = false;
						moveBack = false;
						break;
					}
				}

				if (hit.collider.tag == "3") {
					if (cc.getCurrentTarget() == 2) {
						moveLeft = false;
						moveBack = false;
						break;
					}
				}

				if (hit.collider.tag == "4") {
					if (cc.getCurrentTarget() == 3) {
						moveLeft = false;
						moveBack = false;
						break;
					}
				}
			}

		}

		if (moveBack) {
			moveLeft = true;
		}
	}

	public void checkRight(){
		Vector3 checkDirection;
		bool moveBack = true;
		if (cc.getCurrentTarget() == 0) {
			checkDirection = Vector3.right;
		} else if (cc.getCurrentTarget() == 1) {
			checkDirection = Vector3.forward;
		} else if (cc.getCurrentTarget() == 2) {
			checkDirection = Vector3.left;
		} else {
			checkDirection = Vector3.back;
		}


		Transform[] children = currentBlock.GetComponentsInChildren<Transform> ();
		foreach (Transform child in children){
			Debug.DrawRay(child.position, checkDirection*1, Color.green);
			RaycastHit hit;
			Debug.DrawRay(child.position, checkDirection, Color.green);
			if (Physics.Raycast (child.position, checkDirection, out hit, 1)) {
				if (hit.collider.tag == "pb") {
					moveRight = false;
					moveBack = false;
					break;
				}

				if (hit.collider.tag == "1") {
					if (cc.getCurrentTarget() == 3) {
						moveRight = false;
						moveBack = false;
						break;
					}
				}

				if (hit.collider.tag == "2") {
					if (cc.getCurrentTarget() == 0) {
						moveRight = false;
						moveBack = false;
						break;
					}
				}

				if (hit.collider.tag == "3") {
					if (cc.getCurrentTarget() == 1) {
						moveRight = false;
						moveBack = false;
						break;
					}
				}

				if (hit.collider.tag == "4") {
					if (cc.getCurrentTarget() == 2) {
						moveRight = false;
						moveBack = false;
						break;
					}
				}
			}

		}

		if (moveBack) {
			moveRight = true; 
		}
	}

	public void checkDown(){
		Transform[] children = currentBlock.GetComponentsInChildren<Transform> ();
		foreach (Transform child in children){
			Debug.DrawRay(child.position, Vector3.down*1, Color.green);
			RaycastHit hit;
			Debug.DrawRay(child.position, Vector3.down, Color.green);
			if (Physics.Raycast (child.position, Vector3.down, out hit, 1)) {
				if(hit.collider.tag == "pb" || hit.collider.tag == "Base"){
					nameT = hit.collider.gameObject.name;
					changeFall();
					doCheckDown = false;
					break;
				}
			}

		}
	}

	public void activateChildren(){
		for (int i = 0 ; i < 4; i++) {
			currentBlock.transform.GetChild(i).tag = "pb";
		}
	}

	public void moveAllDown(int ypos){
		GameObject[] placedPieces = GameObject.FindGameObjectsWithTag ("pb");
		foreach (GameObject p in placedPieces) {
			p.transform.position = new Vector3 (p.transform.position.x, p.transform.position.y - 1, p.transform.position.z);
		}
	}


	public void setGameOver(){
		gameOver = true;
		sc.hideScore ();
		nextBlockImage.gameObject.SetActive(false);
		menu.gameObject.SetActive (true);
		restart.gameObject.SetActive (true);
	}

	public void pauseGame(){
		if (!gameOver) {
			pauseText.gameObject.SetActive (true);
			menu.gameObject.SetActive (true);
			restart.gameObject.SetActive (true);
			continueGame.gameObject.SetActive (true);
			Time.timeScale = 0; 
		}
	}

	public void mainMenu(){
		Time.timeScale = 1; 
		SceneManager.LoadScene ("Start Menu");
	}

	public void restartGame(){
		Time.timeScale = 1; 
		SceneManager.LoadScene ("Main Scene");
	}

	public void continueClick(){
		pauseText.gameObject.SetActive (false);
		menu.gameObject.SetActive (false);
		restart.gameObject.SetActive (false);
		continueGame.gameObject.SetActive (false);
		Time.timeScale = 1; 	
	}
}
