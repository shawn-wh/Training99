using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarnText : MonoBehaviour
{
    public float AppearTime = 5f;

    private Rigidbody2D _rigidbody;
    private TMP_Text _warnText;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _warnText = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
        _rigidbody.velocity = new Vector2(0, 0.2f);  //  y= up speed
        // _warnText.SetText("warning");
        Destroy(gameObject, AppearTime);
    }

    public void setContent(string content)
    {
        _warnText.color = new Color32(255, 0, 0, 255);
        _warnText.fontSize = 0.3f;        
        _warnText.SetText("" +  content);
    }
    
}
