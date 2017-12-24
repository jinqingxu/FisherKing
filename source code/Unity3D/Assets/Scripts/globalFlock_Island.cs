using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Project1;
using Database;
public class globalFlock_Island : MonoBehaviour {
	public GameObject fishPrefab;
	public GameObject goalPrefab;
	public static int tankSize=5;
	public static int minIndex;
	public static float curScale;
	public GameObject stick;
	private Bait bait;
	WarnController wc;
	float m_timer;
	//bool isBaiting;
	public static bool isSuccess;
	public static bool isFishing;
	public static bool isBaiting;
	int rotateCount;
	static int numFish=15;
	public static GameObject[] allFish = new GameObject[numFish];
	public static Vector3 goalPos;
	public GameObject ok;
	public int move=0;
	public static int bitIndex = 0;
	//debug
	public hardware h=new hardware();

	//island
	public static float xMin = -15.5f;
	public static float xMax = 9.9f;
	public static float yMin = 3.7f;
	public static float yMax = 3.7f;
	public static float zMin = 26f;
	public static float zMax = 32.5f;
	// Use this for initialization
	void Start () {
		ok.SetActive (false);
		wc = GetComponent<WarnController> ();
		bait = GetComponent<Bait> ();
		Para p = GameController.db.getPara (GameController.curLevel);
		bait.setLevel (p);
		isBaiting = false;
		isFishing = false;
		isSuccess = false;
		for (int i = 0; i < numFish; i++) {
			Vector3 pos = new Vector3 (Random.Range (xMin, xMax),
				Random.Range (yMin, yMax),
				Random.Range (zMin, zMax));
			allFish [i] = (GameObject)Instantiate (fishPrefab, pos, Quaternion.identity);
			flock_Island f = allFish [i].GetComponent<flock_Island> ();
			f.id = i;
			float scale = Random.Range (0.4f, 0.6f);
			allFish [i].transform.localScale = new Vector3 (scale,scale,scale);
			f.scale = scale;
		}
		goalPos = goalPrefab.transform.position;
		m_timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		m_timer += Time.deltaTime;
		//Debug.Log ("time");
		//Debug.Log (m_timer);
		if (m_timer >= 1) {
			if (isBaiting) {
				if (isSuccess) {
					//鱼入桶
					isSuccess = true;
					isFishing = false;
					isBaiting = true;
					rotateCount = 0;
					stick.transform.rotation = Quaternion.Euler (new Vector3 (150, -60, 0));
					bitIndex = minIndex;
				}

				if (isFishing) {
					//竿上抬
					if (rotateCount <= 3) {
						stick.transform.rotation = Quaternion.Euler (new Vector3 (150 + 5*rotateCount, -60, 0));
						rotateCount++;
					}
					else
						stick.transform.rotation = Quaternion.Euler (new Vector3 (160, -60, 0));
				}
			}
			//float force = GameController.board.getPullValue();
			float force=h.getPullValue();
			//Debug.Log (force);
			int res = bait.getStatus (force);
			if (res == 5) {
				isBaiting = true;
				GameController.board.startMotor (GameController.curLevel);
				float minDist = 100;
				for (int i = 0; i < numFish; i++) {
					flock_Island f = allFish [i].GetComponentInChildren<flock_Island> ();
					if (f.dist < minDist) {
						minDist = f.dist;
						minIndex = i;
						curScale = f.scale;
					}
				}
				wc.SendWarning("Wow! The fish is baiting! Keep patient!");
			}

			if (res == 1) {
				Debug.Log ("1");
				isSuccess = true;
				h.startMotor (3);
				wc.SendWarning("Congratulations!");
				float s = (curScale - 0.4f) / (0.6f - 0.4f);
				bool isUpgrade = UserController.UpdateInfo(s);
				if(isUpgrade){
					if (GameController.curLevel == 4) {
						wc.SendWarning ("Congratulations! You are the fishing king!");
					} else {
						wc.SendWarning ("Congratulations! You've updraded!");
						ok.SetActive (true);

						//SceneManager.LoadScene(GameCtroller.curLevel);
					}
				}
			} 

			if (res == 2) {
				isFishing = false;
				isBaiting = false;
				rotateCount = 0;
				stick.transform.rotation = Quaternion.Euler (new Vector3 (150, -60, 0));
				Debug.Log ("2");
				wc.SendWarning("Oops! The bait was eaten up!");
			}

			if (res == 3) {
				isFishing = false;
				isBaiting = false;
				rotateCount = 0;
				stick.transform.rotation = Quaternion.Euler (new Vector3 (150, -60, 0));
				Debug.Log ("3");
				wc.SendWarning ("Oops! The fish was scared away!");
			}

			if (res == 4) {
				Debug.Log ("4");
				isFishing = true;
				wc.SendWarning("up up!");
				rotateCount = 1;
			}


			//Debug.Log ("rotateCount: " + rotateCount);

			//鱼放跑
			if(res==0) {
				Debug.Log (" waiting ");
				minIndex = -1;
			}
			m_timer = 0;
		}
	}
}
