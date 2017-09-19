using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {

	//Clearing one row is worth 100 points
	//Placing a block is worth 10 points

	public Text scoreText;
	public Text goText;
	public Text nextBlock;
	public Image frame;

	private int score;

	// Use this for initialization
	void Start () {
		scoreText.gameObject.SetActive(true);
		scoreText.text = "Score:0";
		goText.text = "";
		nextBlock.text = "Next Block:";
		frame.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void placeBlock(){
		score += 10;
		updateScore ();
	}

	public void clearRow(){
		score += 100;
		updateScore ();
	}

	public void updateScore(){
		scoreText.text = "Score:" + score.ToString ();
	}

	public void hideScore(){
		scoreText.gameObject.SetActive(false);
		goText.text = "Game Over\nScore:" + score.ToString ();
		nextBlock.text = "";
		frame.gameObject.SetActive (false);
	}


}
