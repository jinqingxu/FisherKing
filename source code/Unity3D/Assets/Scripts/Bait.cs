using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Database;

public class Bait : MonoBehaviour {
	public Hook hook;
	private float dis = 0;
	private float foreForce;
	private int initialTime=0;
	private int currentTime=0;

	// 鱼的属性
	private float force = 4f; //力量下限
	private float acc = 10f; //加速度上限
	private int depth = 30;  //鱼的深度
	private int patience = 100; //鱼的耐心 超过该时间游走


	private static DateTime beginTime;
	private int timeGap = 10;
	// Use this for initialization
	void Start () {
		beginTime = DateTime.Now;
		hook = new Hook ();
		hook.beginBait (initialTime);
	}
	
	// Update is called once per frame
	void Update () {
		//getStatus (12);
	}

	/* f - the weight of fish
	*  a - the sensitivity para
	*  d - the depth of pool
	*  pa - the time for fish to eat up bait
	*/
	public void setLevel(Para p)
	{
		force = p.force;
		acc = p.sensitivity;
		depth = p.depth;
		patience = p.patience;
	}

	/*
	 * 0 - the fish don't bait
	 * 1 - successful!
	 * 2 - bait is eaten by fish!
	 * 3 - too fast to get the fish!
	 * 4 - continue
	 * 5 - the fish on the bait!
	*/
	public int getStatus(float f)
	{
		currentTime = currentTime + 1;
		//已经开始钓鱼
		if (hook.getStatus())
		{
			// 鱼饵被吃完
			if (currentTime - initialTime >= patience)
			{
				reStart();
				return 2;
			}
			// 鱼被拉动
			else if (f > force)
			{
				dis = dis + f - force;
				// 鱼被吓跑
				if (f - foreForce > acc)
				{
					reStart();
					return 3;
				}
				foreForce = f;
			}
			if (dis >= depth) //钓上鱼
			{
				reStart();
				return 1;
			}
			return 4;
		}
		//还没开始钓鱼 等待中
		else {
			if (hook.CheckBait(currentTime))
			{
				initialTime = currentTime; //开始计算鱼上钩时间
				foreForce = f;
				return 5;
			}
			return 0;
		}
	}

	//清空状态
	private void reStart()
	{
		foreForce = 0;
		dis = 0;
		initialTime = 0;
		currentTime = 0;
		hook.beginBait(initialTime);
	}

	public int getGapTime()
	{
		DateTime t = DateTime.Now;
		double seconds = t.Subtract(beginTime).TotalSeconds;
		Debug.Log("seconds:"+seconds);
		return (int)seconds;
	}

	Bait(){
		Start ();
	}
}
