using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInfo : MonoBehaviour {
	public Text username;
	public Text level;
	public Text score;
	public Text numFish;
	public Text warning;
	float beginTime;
	float endTime;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		username.text = "Username: " + GameController.username.ToString();
		level.text = "Level: " + GameController.curLevel.ToString();
		score.text = "Score: " + GameController.curScore.ToString();
		numFish.text = GameController.curNumFish.ToString();
	}


}
