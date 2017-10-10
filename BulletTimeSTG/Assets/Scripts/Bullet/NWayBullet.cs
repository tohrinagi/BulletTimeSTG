using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NWayBullet : Bullet
{
	public int speed = 5;
	public bool canAim = false;
    public float bulletNum = 0;
    public float lifeTime = 5;
    public float nWayAngle = 30;
    public float angle = 0;
    public NormalBullet prefab;

	public override void Run()
	{
        float oneAngle = nWayAngle / (bulletNum - 1);
		for (int i = 0; i < bulletNum; ++i)
        {
            PoolableObject obj = ObjectPool.instance.Create(prefab, transform.position, transform.rotation);
            NormalBullet bullet = obj.gameObject.GetComponent<NormalBullet>();
            bullet.canAim = false;
            bullet.speed = speed;
            bullet.lifeTime = lifeTime;

            float bulletAngle = (nWayAngle / 2) - (i * oneAngle);
            if (canAim)
            {
                bulletAngle += GetAim();
            }
            else
            {
                bulletAngle += angle;
            }
			bullet.angle = bulletAngle;
            bullet.Run();
        }

        ReturnToPool();
	}

}
