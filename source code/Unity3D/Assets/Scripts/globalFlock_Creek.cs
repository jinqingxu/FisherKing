using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Database;
using Project1;
public class globalFlock_Creek : MonoBehaviour {
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
	public hardware h = new hardware ();
	public static int bitIndex;
	static int numFish=15;
	public static GameObject[] allFish = new GameObject[numFish];
	public static Vector3 goalPos;
	public GameObject ok;

	//creek
	public static float xMin = 101f;
	public static float xMax = 103.5f;
	public static float yMin = 20.9f;
	public static float yMax = 20.9f;
	public static float zMin = 175f;
	public static float zMax = 178.2f;

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
			flock_Creek f = allFish [i].GetComponent<flock_Creek> ();
			f.id = i;
			float scale = Random.Range (0.2f, 0.6f);
			allFish [i].transform.localScale = new Vector3 (scale,scale,scale);
			f.scale = scale;
		}
		goalPos = goalPrefab.transform.position;
		m_timer = 0;
		//Debug.Log ("goalPos: (" + goalPos.x + ", " + goalPos.y + ", " + goalPos.z + ")!");

		/*GameController.db = new DataAccess ();
		GameController.curLevel = GameController.db.getUserLevel (GameController.username);
		GameController.curScore = GameController.db.getUserScore (GameController.username);
		GameController.curNumFish = 0;*/
	}

	// Update is called once per frame
	void Update () {
		m_timer += Time.deltaTime;
		if (m_timer >= 1) {
			float force = h.getPullValue ();
			//Debug.Log (force);
			int res = bait.getStatus (force);
			if (res == 5) {
				isBaiting = true;
				wc.SendWarning("Wow! The fish is baiting! Keep patient!");
				GameController.board.startMotor (GameController.curLevel);
				float minDist = 100;
				for (int i = 0; i < numFish; i++) {
					flock_Creek f = allFish [i].GetComponentInChildren<flock_Creek> ();
					if (f.dist < minDist) {
						minDist = f.dist;
						minIndex = i;
						curScale = f.scale;
					}
				}
			}

			if (res == 1) {
				//Debug.Log ("1");
				isSuccess = true;
				//string mada = GameController.board.getMada ();
				wc.SendWarning("Congratulations!");
				float s = (curScale - 0.2f) / (0.6f - 0.2f);
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
			
				return;
			} 

			if (res == 2) {
				isFishing = false;
				isBaiting = false;
				rotateCount = 0;
				stick.transform.rotation = Quaternion.Euler (new Vector3 (110, -40, 0));
				//Debug.Log ("2");
				wc.SendWarning("Oops! The bait was eaten up!");
			}

			if (res == 3) {
				isFishing = false;
				isBaiting = false;
				rotateCount = 0;
				stick.transform.rotation = Quaternion.Euler (new Vector3 (110, -40, 0));
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
					isSuccess = false;
					isFishing = false;
					isBaiting = false;
					rotateCount = 0;
					stick.transform.rotation = Quaternion.Euler (new Vector3 (110, -40, 0));
					bitIndex = minIndex;

				}
				if (isFishing) {
					//竿上抬
					if (rotateCount <= 3) {
						stick.transform.rotation = Quaternion.Euler (new Vector3 (110 + 5*rotateCount, -40, 0));
						rotateCount++;
					}
					else
						stick.transform.rotation = Quaternion.Euler (new Vector3 (120, -40, 0));
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
