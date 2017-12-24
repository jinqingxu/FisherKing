using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
using UnityEngine.SceneManagement;

public class globalFlock_City : MonoBehaviour {
	public GameObject fishPrefab;
	public GameObject goalPrefab;
	public static int tankSize=5;
	public static int minIndex;
	public static float curScale;
	public GameObject stick;
	private Bait bait;
	WarnController wc;
	float m_timer;
	public static bool isBaiting;
	public static bool isSuccess;
	public static bool isFishing;
	int rotateCount;
	public GameObject ok;

	static int numFish=15;
	public static GameObject[] allFish = new GameObject[numFish];
	public static Vector3 goalPos;

	//city
	public static float xMin = -55f;
	public static float xMax = -10f;
	public static float yMin = 238f;
	public static float yMax = 245f;
	public static float zMin = 428f;
	public static float zMax = 440f;

	public static int bitIndex;

	// Use this for initialization
	void Start () {
		wc = GetComponent<WarnController> ();
		bait = GetComponent<Bait> ();
		Para p = GameController.db.getPara (GameController.curLevel);
		bait.setLevel (p);
		ok.SetActive (false);
		isBaiting = false;
		isFishing = false;
		isSuccess = false;
		for (int i = 0; i < numFish; i++) {
			Vector3 pos = new Vector3 (Random.Range (xMin, xMax),
				Random.Range (yMin, yMax),
				Random.Range (zMin, zMax));
			allFish [i] = (GameObject)Instantiate (fishPrefab, pos, Quaternion.identity);
			flock_City f = allFish [i].GetComponent<flock_City> ();
			f.id = i;
			float scale = Random.Range (2f, 4f);
			allFish [i].transform.localScale = new Vector3 (scale,scale,scale);
			f.scale = scale;
		}
		goalPos = goalPrefab.transform.position;
		m_timer = 0;
		//Debug.Log ("goalPos: (" + goalPos.x + ", " + goalPos.y + ", " + goalPos.z + ")!");
	}


	// Update is called once per frame
	void Update () {
		m_timer += Time.deltaTime;
		if (m_timer >= 1) {
			float force = GameController.board.getPullValue();
			Debug.Log (force);
			int res = bait.getStatus (force);
			if (res == 5) {
				isBaiting = true;
				wc.SendWarning("Wow! The fish is baiting! Keep patient!");
				GameController.board.startMotor (GameController.curLevel);
				float minDist = 100;
				for (int i = 0; i < numFish; i++) {
					flock_City f = allFish [i].GetComponentInChildren<flock_City> ();
					if (f.dist < minDist) {
						minDist = f.dist;
						minIndex = i;
						curScale = f.scale;
					}
				}
			}

			if (res == 1) {
				isSuccess = true;
				wc.SendWarning("Congratulations!");
				float s = (curScale - 2f) / (4f-2f);
				bool isUpgrade = UserController.UpdateInfo(s);
				if(isUpgrade){
					if (GameController.curLevel == 4) {
						wc.SendWarning ("Congratulations! You are the fishing king!");
					} else {
						wc.SendWarning ("Congratulations! You've updraded!");
						ok.SetActive (true);
						//SceneManager.LoadScene(GameController.curLevel);
					}
				}
			} 

			if (res == 2) {
				isFishing = false;
				isBaiting = false;
				rotateCount = 0;
				stick.transform.rotation = Quaternion.Euler (new Vector3 (0, -90, 140));
				//Debug.Log ("2");
				wc.SendWarning("Oops! The bait was eaten up!");

			}

			if (res == 3) {
				isFishing = false;
				isBaiting = false;
				rotateCount = 0;
				stick.transform.rotation = Quaternion.Euler (new Vector3 (0, -90, 140));
				//Debug.Log ("3");
				wc.SendWarning ("Oops! The fish was scared away!");
			}

			if (res == 4) {
				//Debug.Log ("4");
				isFishing = true;
				rotateCount = 1;
			}


			Debug.Log ("rotateCount: " + rotateCount);
			if (isBaiting) {
				if (isSuccess) {
					//鱼入桶
					isSuccess = true;
					isFishing = false;
					isBaiting = true;
					rotateCount = 0;
					stick.transform.rotation = Quaternion.Euler (new Vector3 (0, -90, 140));
					bitIndex = minIndex;

				}
				if (isFishing) {
					//竿上抬
					if (rotateCount <= 3) {
						stick.transform.rotation = Quaternion.Euler (new Vector3 (0, -90, 140+10*rotateCount));
						rotateCount++;
					}
					else
						stick.transform.rotation = Quaternion.Euler (new Vector3 (0, -90, 150));
				}
			}

			//鱼放跑
			else {
				//Debug.Log (" waiting ");
				minIndex = -1;
			}
			m_timer = 0;
		}
	}
}
