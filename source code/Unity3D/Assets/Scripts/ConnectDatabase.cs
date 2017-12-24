using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
using Project1;

public class ConnectDatabase : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameController.db = new DataAccess ();
		GameController.board = new hardware ();
		GameController.curLevel = GameController.db.getUserLevel (GameController.username);
		GameController.curScore = GameController.db.getUserScore (GameController.username);
		GameController.curNumFish = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onExit(){
		Debug.Log ("Close Application!");
		Application.Quit ();
	}
}
