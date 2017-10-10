using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class NormalBullet : Bullet
{
	public int speed = 5;
	public bool canAim = false;
    public float angle = 0;
    public float lifeTime = 5;

	public override void Init()
	{
    }
	public override void Run()
	{
		StartCoroutine("StartUpdate");
	}
	IEnumerator StartUpdate()
	{
		Player player = FindObjectOfType<Player>();
        if (canAim && player != null)
        {
            Vector3 dif = player.gameObject.transform.position - transform.position;

            GetComponent<Rigidbody2D>().velocity = dif.normalized * speed;
		}
        else
        {
            GetComponent<Rigidbody2D>().velocity = GetVelocity(angle, speed);
        }

		yield return new WaitForSeconds(lifeTime);

		ReturnToPool();
	}

	// ぶつかった瞬間に呼び出される
	void OnTriggerEnter2D(Collider2D c)
	{
		//ReturnToPool();
	}
}
