using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_BulletGenerator : MonoBehaviour
{
    public float m_X = 0;
    public float m_Y = 0;

    private float m_Time = 0;

    public static LV_BulletGenerator bulletsPoolInstance;
    public GameObject bulletObj;
    public int pooledAmount = 1;

    private List<GameObject> pool;

    private int currIdx = 0;

    private void Awake()
    {
        m_Time = Time.time + Random.Range(0.5f, 1.5f);
        bulletsPoolInstance = this;
    }

    private void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(bulletObj);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPoolObj()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            int tempI = (currIdx + i) % pool.Count;
            if (!pool[tempI].activeInHierarchy)
            {
                currIdx = (tempI + 1) % pool.Count;
                return pool[tempI];
            }
        }

        return null;
    }

    public void NextTime()
    {
        //m_Time = Time.time + Random.Range(0.5f, 1.5f);
        m_Time = Time.time + Random.Range(1f, 5f);
    }

    public bool CheckTime()
    {
        if (m_Time < Time.time)
        {
            return true;
        }

        return false;
    }

    public Vector3 GetRandomPos()
    {
        float xMin = transform.position.x - m_X * 0.5f;
        float xMax = transform.position.x + m_X * 0.5f;
        float yMin = transform.position.y - m_Y * 0.5f;
        float yMax = transform.position.y + m_Y * 0.5f;

        Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);


        return pos;
    }
}
