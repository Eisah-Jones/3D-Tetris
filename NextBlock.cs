using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextBlock : MonoBehaviour {

	public Sprite[] blocks;

	public void changeSprite(int i){
		GetComponent<Image> ().sprite = blocks [i];
	}
}
