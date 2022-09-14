using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArea : MonoBehaviour
{
    public float m_X = 0;
    public float m_Y = 0;

    private float m_Time = 0;

    private void Awake()
    {
        m_Time = Time.time + Random.Range(0.5f, 1.5f);
    }

    public void NextTime()
    {
        m_Time = Time.time + Random.Range(0.5f, 1.5f);
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
