using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_TutorialBulletBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other) 
    {
        
        Debug.Log("OnTriggerExit");
        Debug.Log(other.tag);
        if (other.tag != "Player") 
        {
            
            Debug.Log("OnTriggerExit NOT PLAYER");
            other.transform.position = new Vector3((float)0.52, (float)7.42, 0);
            other.GetComponent<SpriteRenderer>().color = new Color32(153, 0, 0, 255);
            
        }
    
    }
}
