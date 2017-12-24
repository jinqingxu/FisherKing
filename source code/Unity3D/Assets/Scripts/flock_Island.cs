using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock_Island : MonoBehaviour {
	public float speed = 0.001f;
	float rotationSpeed = 4.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;
	float neighbourDistance=3.0f;
	public int id;
	public float scale;
	// Use this for initialization
	bool turning=false;
	Vector3 center;
	public float dist;
	int move=1;
	void Start () {
		speed = Random.Range (0.5f, 1);
		center = new Vector3 ((globalFlock_Island.xMin + globalFlock_Island.xMax) / 2,
			(globalFlock_Island.yMin + globalFlock_Island.yMax) / 2,
			(globalFlock_Island.zMin + globalFlock_Island.zMax) / 2);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		dist = Vector3.Distance (pos, globalFlock_Island.goalPos);
		if (globalFlock_Island.bitIndex == id) {
			Debug.Log ("my id: " + id + ", bitIndex: " + globalFlock_Island.bitIndex);
			if (globalFlock_Island.isSuccess == true) {
				//stick.transform.rotation = Quaternion.Euler (new Vector3 (150, -60, 0));
				//if(move==-1)bitIndex = minIndex;
				//move = 1;

				/*float start_x = allFish [bitIndex].transform.position.x;
				float start_y = allFish [bitIndex].transform.position.y;
				float start_z = allFish [bitIndex].transform.position.z;*/
				float start_x = transform.position.x;
				float start_y = transform.position.y;
				float start_z = transform.position.z;
				float end_x = -5.57f;
				float end_y = 5.16f;
				float end_z = 34.14f;
				if (Mathf.Sqrt (Mathf.Pow ((start_x - end_x), 2) + Mathf.Pow ((start_y - end_y), 2) + Mathf.Pow ((start_z - end_z), 2)) < 2) {
					move = -1;
					globalFlock_Island.isSuccess = false;
					globalFlock_Island.isBaiting = false;
					//stick.transform.rotation = Quaternion.Euler (new Vector3 (150, -60, 0));
					Destroy (gameObject);
					return;
				}
				if (move == 1) {
					float vw = 0.12f;
					//float ws = Mathf.Sqrt (Mathf.Pow (start_x, 2) + Mathf.Pow (start_z, 2));
					//float angle1 = Mathf.Abs (start_x) / ws;
					//float angle2 = Mathf.Abs (start_z) / ws;
					//float angle1 = 0.27f;
					//float angle2 = 0.963f;
					float angle1=0.23f;
					float angle2 = 0.972f;
					float vx = vw * angle1;
					float vz = vw * angle2;
					float new_x = 0;
					float new_y = 0;
					float new_z = 0;
					if (start_x < end_x) {
						new_x = start_x + vx;

					} else {
						new_x = start_x - vx;
					}
					if (start_z < end_z) {
						new_z = start_z + vz;
					} else {
						new_z = start_z - vz;
					}
					float new_w = Mathf.Sqrt (Mathf.Pow (new_x, 2) + Mathf.Pow (new_z, 2));
					//new_y = (-0.237f) * Mathf.Pow (new_w, 2) + 15.33f * new_w - 241.17f;
					new_y = (-0.24f) * Mathf.Pow (new_w, 2) + 15.68f * new_w - 249f;
					transform.localPosition = new Vector3 (new_x, new_y, new_z);
					return;
				}

			}


		}
		if (globalFlock_Island.minIndex == id) {
			Debug.Log ("my id: " + id + ", minIndex: " + globalFlock_Island.minIndex);
			transform.localPosition = new Vector3 (-4.23f, 3.84f, 28.83f);
			transform.localRotation = Quaternion.Euler (new Vector3 (0, -90, 0)); 
			/*if (globalFlock_Island.isSuccess == true) {
				//stick.transform.rotation = Quaternion.Euler (new Vector3 (150, -60, 0));
				//if(move==-1)bitIndex = minIndex;
				//move = 1;

				float start_x = allFish [bitIndex].transform.position.x;
				float start_y = allFish [bitIndex].transform.position.y;
				float start_z = allFish [bitIndex].transform.position.z;
				float start_x = transform.position.x;
				float start_y = transform.position.y;
				float start_z = transform.position.z;
				float end_x = -5.57f;
				float end_y = 5.16f;
				float end_z = 34.14f;
				if (Mathf.Sqrt (Mathf.Pow ((start_x - end_x), 2) + Mathf.Pow ((start_y - end_y), 2) + Mathf.Pow ((start_z - end_z), 2)) < 1.5) {
					move = -1;
					globalFlock_Island.isSuccess = false;

					//stick.transform.rotation = Quaternion.Euler (new Vector3 (150, -60, 0));
					Destroy (gameObject);
					return;
				}
				if (move == 1) {
					float vw = 0.08f;
					//float ws = Mathf.Sqrt (Mathf.Pow (start_x, 2) + Mathf.Pow (start_z, 2));
					//float angle1 = Mathf.Abs (start_x) / ws;
					//float angle2 = Mathf.Abs (start_z) / ws;
					//float angle1 = 0.27f;
					//float angle2 = 0.963f;
					float angle1=0.23f;
					float angle2 = 0.972f;
					float vx = vw * angle1;
					float vz = vw * angle2;
					float new_x = 0;
					float new_y = 0;
					float new_z = 0;
					if (start_x < end_x) {
						new_x = start_x + vx;

					} else {
						new_x = start_x - vx;
					}
					if (start_z < end_z) {
						new_z = start_z + vz;
					} else {
						new_z = start_z - vz;
					}
					float new_w = Mathf.Sqrt (Mathf.Pow (new_x, 2) + Mathf.Pow (new_z, 2));
					//new_y = (-0.237f) * Mathf.Pow (new_w, 2) + 15.33f * new_w - 241.17f;
					new_y = (-0.24f) * Mathf.Pow (new_w, 2) + 15.68f * new_w - 249f;
					transform.localPosition = new Vector3 (new_x, new_y, new_z);
					return;
				}

			}*/
		} else {
			if ((pos.x>=globalFlock_Island.xMin&&pos.x<=globalFlock_Island.xMax)&&
				(pos.y>=globalFlock_Island.yMin&&pos.y<=globalFlock_Island.yMax)&&
				(pos.z>=globalFlock_Island.zMin&&pos.z<=globalFlock_Island.zMax))
				turning = false;
			else
				turning = true;
			if (turning) {
				Vector3 direction = center - transform.position;
				transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (direction),
					rotationSpeed * Time.deltaTime);
				speed = Random.Range (0.2f, 0.8f);
			} else {
				//if (Random.Range (0, 5) < 1)
					//ApplyRules ();
			}
			transform.Translate (Random.Range(0,0.05f)*speed, 0, Random.Range(0,0.05f) * speed);
		}
	}

	void ApplyRules(){
		GameObject[] gos;
		gos = globalFlock_Island.allFish;
		Vector3 vcenter = center;
		Vector3 vavoid = center;
		float gSpeed = 0.1f;
		Vector3 goalPos = globalFlock_Island.goalPos;
		float dist;
		int groupSize = 0;
		foreach (GameObject go in gos) {
			if (go != this.gameObject) {
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= neighbourDistance) {
					vcenter += go.transform.position;
					groupSize++;
					if (dist < 1.0f)
						vavoid = vavoid + (this.transform.position - go.transform.position);
					flock_Island anotherFlock = go.GetComponent<flock_Island> ();
					gSpeed = gSpeed + anotherFlock.speed;
				}
			}
		}
		if (groupSize > 0) {
			vcenter = vcenter / groupSize + (goalPos - this.transform.position);
			speed = gSpeed / groupSize;
			Vector3 direction = (vcenter + vavoid) - transform.position;
			if (direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (direction),
					rotationSpeed * Time.deltaTime);
		}
	}
}
