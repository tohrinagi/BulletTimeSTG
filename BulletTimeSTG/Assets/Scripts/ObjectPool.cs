using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
	private static ObjectPool _instance;

	// シングルトン
	public static ObjectPool instance
	{
		get
		{
			if (_instance == null)
			{

				// シーン上から取得する
				_instance = FindObjectOfType<ObjectPool>();

                if (_instance == null)
                {

                    // ゲームオブジェクトを作成しObjectPoolコンポーネントを追加する
                    _instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
                }
			}
			return _instance;
		}
	}

	public int PoolSize = 200;
	private Dictionary<int, List<PoolableObject>> pooledObjects = new Dictionary<int, List<PoolableObject>>();

	// ゲームオブジェクトをpooledGameObjectsから取得する。必要であれば新たに生成する
	public PoolableObject Create(PoolableObject prefab, Vector2 position, Quaternion rotation)
	{
        int key = prefab.gameObject.GetInstanceID();

		// Dictionaryにkeyが存在しなければ作成する
		if (pooledObjects.ContainsKey(key) == false)
		{
            pooledObjects.Add(key, new List<PoolableObject>(PoolSize));
		}

        List<PoolableObject> objects = pooledObjects[key];
        PoolableObject obj;
		for (int i = 0; i < objects.Count; i++)
		{
        	obj = objects[i];
            if (obj.gameObject.activeInHierarchy == false)
            {
				obj.gameObject.transform.position = position;
				obj.gameObject.transform.rotation = rotation;
				obj.gameObject.SetActive(true);
                obj.Init();
				return obj;
			}
		}

		obj = Instantiate(prefab, position, rotation);
        objects.Add(obj);
		obj.gameObject.transform.parent = transform;
		obj.Init();
		return obj;
	}

	// ゲームオブジェクトを非アクティブにする。こうすることで再利用可能状態にする
    public void Release(PoolableObject obj)
	{
		obj.gameObject.SetActive(false);
	}
}