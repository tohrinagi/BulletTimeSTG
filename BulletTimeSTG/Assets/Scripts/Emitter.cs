using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {

	// Waveプレハブを格納する
	public GameObject[] waves;

	// 現在のWave
	private int currentWave;

    // Wave開始
    private bool isStart = false;

	// Managerコンポーネント
	private Manager manager;

    public void StartWave() {
        isStart = true;
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

				for (int k = 0; k < gameObject.transform.childCount; k++)
				{
					GameObject bullet = gameObject.transform.GetChild(k).gameObject;
					Destroy(bullet);
				}
				Destroy(enemy);
			}
			Destroy(wave);
		}
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
			GameObject wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);
			// WaveをEmitterの子要素にする
			wave.transform.parent = transform;

        } 

		// 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
		if (waves.Length <= ++currentWave)
		{
			currentWave = 0;
		}
	}
}
