using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarnText : MonoBehaviour
{
    public float AppearTime = 0.1f;

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
        _warnText.color = new Color32(0, 0, 0, 255);
        _rigidbody.velocity = new Vector2(0, 0.4f);  //  y= up speed
        _warnText.SetText("Color changing !!!");
        _warnText.fontSize = 0.3f;
        Destroy(gameObject, AppearTime);
    }

    // Update is called once per frame
    
}
