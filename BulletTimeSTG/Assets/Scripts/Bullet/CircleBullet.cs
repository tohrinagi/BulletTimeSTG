using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : Bullet
{
	public int speed = 5;
	public float lifeTime = 5;
	public float space = 6;
	public NormalBullet prefab;
	public float awake = 0;
	public float accer = 0;
	public float curve = 0;

	public override void Run()
	{
		for (float i = 0; i < 360; i+=space)
		{
			PoolableObject obj = ObjectPool.instance.Create(prefab, transform.position, transform.rotation);
			NormalBullet bullet = obj.gameObject.GetComponent<NormalBullet>();
			bullet.canAim = false;
			bullet.speed = speed;
			bullet.lifeTime = lifeTime;
			bullet.angle = i;
			bullet.awake = awake;
			bullet.accer = accer;
			bullet.curve = curve;
			bullet.Run();
		}

		ReturnToPool();
	}

}
