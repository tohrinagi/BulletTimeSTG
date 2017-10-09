using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move01 : MonoBehaviour {
	// Spaceshipコンポーネント
	Spaceship spaceship;

    Transform parent;

    public GameObject bullet;

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

        Instantiate(bullet, parent.position, parent.rotation);
	}
}
