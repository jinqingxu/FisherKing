using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WarnController : MonoBehaviour {
	public Text warning;
	float beginTime;
	// Use this for initialization
	void Start () {
		disableWarning ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void enableWarning (string warn){
		warning.enabled = true;
		warning.text = warn.ToString ();
	}

	void disableWarning (){
		warning.text = "";
		warning.enabled = false;
	}

	public void SendWarning (string warn){
		//beginTime = Time.time;
		enableWarning (warn);
		//while ((Time.time - beginTime) < 1.5f)
		//	;
		//disableWarning ();
	}

	public void ChangeScene(){
		SceneManager.LoadScene (GameController.curLevel);
	}
}
