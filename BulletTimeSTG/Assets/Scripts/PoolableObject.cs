using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    public abstract void Init();

	protected new void Destroy(Object obj)
	{
		ReturnToPool();
	}

	public void ReturnToPool()
	{
        ObjectPool.instance.Release(this);
	}
}