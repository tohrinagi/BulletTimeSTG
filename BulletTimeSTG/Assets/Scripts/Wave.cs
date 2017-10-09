using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public void SetDifficulty(float difficulty) {
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			GameObject enemy = gameObject.transform.GetChild(i).gameObject;
            enemy.GetComponent<Enemy>().SetDifficulty(difficulty);
		}
    }
}
