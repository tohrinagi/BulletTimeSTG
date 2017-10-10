using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableObject
{
	public float GetAim()
	{
		Player player = FindObjectOfType<Player>();
        Vector3 target = new Vector3(0, -3, 0);
        if( player) {
            target = player.gameObject.transform.position; 
        }

        float dx = target.x - transform.position.x;
        float dy = target.y - transform.position.y;
		return Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;
	}

    public Vector2 GetVelocity(float angle, float speed) {
		float rad = angle * Mathf.Deg2Rad;
		return new Vector2(Mathf.Sin(rad) * speed, Mathf.Cos(rad) * speed);
	}

    public override void Init()
    {
    }

    public virtual void Run() {
        
    }
}
