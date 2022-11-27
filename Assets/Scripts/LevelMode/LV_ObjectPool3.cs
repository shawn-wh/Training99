using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_ObjectPool3 : MonoBehaviour
{
    public static LV_ObjectPool3 poolInstance; // instance of the class

    [SerializeField] private GameObject targetObject;
    private bool needMorePoolingObj = true;
    private List<GameObject> pooledObjs;

    private void Awake()
    {
        poolInstance = this;    // instance of the class
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        pooledObjs = new List<GameObject>();
    }

    public GameObject GetObjectFromPool()
    {
        // When enough objects in the pool
        if (pooledObjs.Count > 0)
        {
            for (int i = 0; i < pooledObjs.Count; i++)
            {
                if (!pooledObjs[i].activeInHierarchy)
                {
                    return pooledObjs[i];
                }
            }
        }
        // When not enough objects in the pool ( pooledObjs.Count <= 0)
        if (needMorePoolingObj)
        {
            GameObject newObj = Instantiate(targetObject);
            newObj.SetActive(false);
            pooledObjs.Add(newObj);
            return newObj;
        }
        return null;
    }

}
