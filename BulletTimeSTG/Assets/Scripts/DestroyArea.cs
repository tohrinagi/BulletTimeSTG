﻿using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Bullet")
        {
            c.gameObject.GetComponent<Bullet>().ReturnToPool();
        }
        else
        {
            Destroy(c.gameObject);
        }
    }
}
