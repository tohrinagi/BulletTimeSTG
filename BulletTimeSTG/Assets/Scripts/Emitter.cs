using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {

	// Waveプレハブを格納する
	public GameObject[] waves;

    // Wave開始
    private bool isStart = false;

	// Managerコンポーネント
	private Manager manager;

    // waveCuont
    private int waveCount;

    public void StartWave() {
        isStart = true;
        waveCount = 0;
    }

    public void StopWave() {
        isStart = false;
	}

    public void ResetWave() {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject wave = gameObject.transform.GetChild(i).gameObject;

            for (int j = 0; j < gameObject.transform.childCount; j++)
            {
                GameObject enemy = gameObject.transform.GetChild(j).gameObject;
				Destroy(enemy);
			}
			Destroy(wave);
		}

		var bullets = GameObject.FindGameObjectsWithTag("Bullet");
		foreach (GameObject bullet in bullets)
		{
			Destroy(bullet);
		}
    }
    float CalcDiffculty(){
        return 1; //waveCount + (waveCount / 3) * 5;
    }

	void Update()
	{
        if (!isStart) { return; }

        // Waveがあれば実行し、敵がないならば削除
		if (gameObject.transform.childCount >= 0)
		{
			bool isEndWave = true;
			for (int i = 0; i < gameObject.transform.childCount; i++)
			{
				GameObject wave = gameObject.transform.GetChild(i).gameObject;
				if (wave.transform.childCount > 0)
				{
					isEndWave = false;
				}
			}
			if (isEndWave)
			{
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					GameObject wave = gameObject.transform.GetChild(i).gameObject;
					// Waveの削除
					Destroy(wave);
				}
			}
		}

        if ( gameObject.transform.childCount == 0 )
        {
			GameObject wave = (GameObject)Instantiate(GetWavePrefab(), transform.position, Quaternion.identity);
            wave.GetComponent<Wave>().SetDifficulty(CalcDiffculty());
			// WaveをEmitterの子要素にする
			wave.transform.parent = transform;

        }

        ++waveCount;	
    }

    GameObject GetWavePrefab() {
        var index = Random.Range(0, waves.Length - 1);
        return waves[index];
    }
}

