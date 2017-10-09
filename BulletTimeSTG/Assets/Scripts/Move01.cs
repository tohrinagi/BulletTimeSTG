using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move01 : MonoBehaviour {
	// Spaceshipコンポーネント
	Spaceship spaceship;

    Transform parent;

	public Bullet bullet;
	// 弾を撃つ間隔
	public float shotDelay;

	IEnumerator Start()
	{
        parent = transform.parent;

		// Spaceshipコンポーネントを取得
		spaceship = parent.gameObject.GetComponent<Spaceship>();

        spaceship.Move(parent.up * -1);

        while( parent.position.y >= 1 ) {
            yield return new WaitForEndOfFrame();
        }
        spaceship.MoveStop();

		while (true)
		{
            ObjectPool.instance.Create(bullet, parent.position,Quaternion.Euler(0,0,180));
            yield return new WaitForSeconds(shotDelay);
        }
	}
}
