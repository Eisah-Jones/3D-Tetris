using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonControls : MonoBehaviour {

	public Button play;
	public Button howTo;
	public Button back;
	public Image info;

	private MainMenuCamera mmc;

	// Use this for initialization
	void Start () {
		mmc = Camera.main.GetComponent<MainMenuCamera> ();
		play.onClick.AddListener (OnPlay);
		howTo.onClick.AddListener (OnHowTo);
		back.onClick.AddListener (OnBack);
		back.gameObject.SetActive (false);
		info.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnHowTo(){
		mmc.moveTo ();
		howTo.gameObject.SetActive (false);
		play.gameObject.SetActive (false);
		StartCoroutine (showInfo());
	}

	private IEnumerator showInfo(){
		yield return new WaitForSeconds (1.1f);
		back.gameObject.SetActive (true);
		info.gameObject.SetActive (true);
	}

	void OnBack(){
		mmc.moveBack ();
		howTo.gameObject.SetActive (true);
		back.gameObject.SetActive (false);
		play.gameObject.SetActive (true);
		info.gameObject.SetActive (false);
	}

	void OnPlay(){
		SceneManager.LoadScene ("Main Scene");
	}
}
