using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hook : MonoBehaviour {
	static int beginTime = 0;
	static int baitTime = 0;
	static int region = 10;
	private bool status = false;

	// true: fishing..
	// false：waiting
	public bool getStatus()
	{
		return status;
	}

	//检查鱼是否咬饵
	public bool CheckBait(int time)
	{
		if (baitTime == 0)
			baitTime = Random.Range (4, region);
		if (time - beginTime < baitTime)
			return false;
		else {
			//beginBait(time);
			status = true;
			return true; //调用鱼咬饵动画
		}

	}

	//一次钓鱼结束后调用
	//开始新一次钓鱼等待
	public void beginBait(int time)
	{
		status = false;
		beginTime = time;
		baitTime = Random.Range (1, region);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
