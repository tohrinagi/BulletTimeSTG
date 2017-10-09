using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Spaceshipコンポーネント
	Spaceship spaceship;

	// ヒットポイント
	public int BaseHp = 1;

	// スコアのポイント
	public int BasePoint = 100;

    // 動作方法
    public GameObject[] moves;


    int currentMove = 0;

    int hp = 1;
    int point = 100;
        
    public void SetDifficulty(float difficulty ) {
        hp = (int)(BaseHp * difficulty);
        point = (int)(BaseHp * difficulty);
    }


	IEnumerator Start()
	{
		// Spaceshipコンポーネントを取得
		spaceship = GetComponent<Spaceship>();

        currentMove = 0;

        while (true)
        {
            GameObject move = (GameObject)Instantiate(moves[currentMove], transform.position, transform.rotation);
　 　       move.transform.parent = transform;

            while (move.activeSelf)
			{
				yield return new WaitForEndOfFrame();
			}
            Destroy(move);

			if (moves.Length >= ++currentMove)
			{
				currentMove = 0;
			}

            yield return new WaitForEndOfFrame();
        }
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		// レイヤー名を取得
		string layerName = LayerMask.LayerToName(c.gameObject.layer);

		// レイヤー名がBullet (Player)以外の時は何も行わない
		if (layerName != "Bullet(Player)") return;

		// Bulletコンポーネントを取得
        Bullet bullet = c.gameObject.GetComponent<Bullet>();

		// ヒットポイントを減らす
		hp = hp - bullet.power;

        // 弾の削除
        bullet.ReturnToPool();

		// ヒットポイントが0以下であれば
		if (hp <= 0)
		{
			// スコアコンポーネントを取得してポイントを追加
			FindObjectOfType<Score>().AddPoint(point);

            // 爆発
			spaceship.Explosion();

			// エネミーの削除
			Destroy(gameObject);
		}
        else
		{

			spaceship.GetAnimator().SetTrigger("Damage");

		}   
    }
}
