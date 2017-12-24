using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController {
	public static bool UpdateInfo(float ratio){
		GameController.curNumFish++;
		int s = (int)Mathf.Round (ratio * 4);
		if (s == 0)
			s = 1;
		GameController.curScore+=s;
		if(GameController.curScore>=GameController.db.getScore(GameController.curLevel)){
			GameController.curLevel++;
			GameController.curNumFish=0;
			return true;
		 }
		return false;	
	}
}
