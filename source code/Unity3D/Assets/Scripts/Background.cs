using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour {
	public Image img1;
	public Text txt1;
	public Image img2;
	public Text txt2;
	public Text txt3;
	public Image img4;
	public Text txt4;
	//public Button btn;
	public GameObject infoPage;
	// Use this for initialization
	void Start () {
		/*img1.enabled = false;
		txt1.enabled = false;
		img2.enabled = false;
		txt2.enabled = false;
		txt3.enabled = false;
		img4.enabled = false;
		txt4.enabled = false;
		btn.*/
		infoPage.SetActive (false);
	}

	public void startGame(){
		SceneManager.LoadScene (GameController.curLevel);
	}

	public void openInfo(){
		infoPage.SetActive (true);
		img2.enabled = false;
		txt2.enabled = false;
		txt3.enabled = false;
		img4.enabled = false;
		txt4.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Next(){
		if (img1.enabled == true) {
			img1.enabled = false;
			txt1.enabled = false;
			img2.enabled = true;
			txt2.enabled = true;
		} else if (txt2.enabled == true) {
			txt2.enabled = false;
			txt3.enabled = true;
		} else if (txt3.enabled == true) {
			txt3.enabled = false;
			img2.enabled = false;
			img4.enabled = true;
			txt4.enabled = true;
		} else if (txt4.enabled == true) {
			infoPage.SetActive (false);
		}
	}
}
