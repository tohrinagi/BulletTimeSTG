using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerBullet : PoolableObject
{
	public int speed = 5;

	// ゲームオブジェクト生成から削除するまでの時間
	public float lifeTime = 5;

	// 攻撃力
	public int power = 1;

	public override void Init()
	{
		StartCoroutine("StartUpdate");
	}

	IEnumerator StartUpdate()
	{
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;

		yield return new WaitForSeconds(lifeTime);

		ReturnToPool();
	}

	// ぶつかった瞬間に呼び出される
	void OnTriggerEnter2D(Collider2D c)
	{
		//ReturnToPool();
	}
}
