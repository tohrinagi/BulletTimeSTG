using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class NormalBullet : Bullet
{
	public float speed = 5;
	public bool canAim = false;
    public float angle = 0;
    public float lifeTime = 5;
    public float awake = 0;
	public float accer = 0;
    public float curve = 0;
    private bool isAwake = false;

	public override void Init()
	{
    }
	public override void Run()
	{
        isAwake = false;
		StartCoroutine("StartUpdate");
	}
	IEnumerator StartUpdate()
	{
		yield return new WaitForSeconds(awake);
        isAwake = true;

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

	void FixedUpdate()
	{
        if (isAwake)
        {
            if (curve != 0) {
                float curveAngle = 0;
                float curveSpeed = 0;
                GetAngleAndSpeed(GetComponent<Rigidbody2D>().velocity, ref curveAngle, ref curveSpeed);
                GetComponent<Rigidbody2D>().velocity = GetVelocity(curveAngle + curve, curveSpeed);
			}
            
            if (accer > 0)
            {
                Vector3 norm = GetComponent<Rigidbody2D>().velocity.normalized;
                GetComponent<Rigidbody2D>().AddForce(norm * accer);
            }
        }
	}

	// ぶつかった瞬間に呼び出される
	void OnTriggerEnter2D(Collider2D c)
	{
		//ReturnToPool();
	}
}
