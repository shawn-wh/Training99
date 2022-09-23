using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{

    // adjust x, y to adjust angle 
    public float pop_range_y = 5f;
    public float pop_range_x = 5f;
    public float FloatTime = 0.8f;  // 0.8 sec
    
    private Rigidbody2D _rigidbody;
    private TMP_Text _floatText;
    private bool isPositiveValue = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _floatText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        // Show damage value
        if (!isPositiveValue)
        {
            _floatText.color = new Color32(255, 0, 0, 255);
            _rigidbody.velocity = new Vector2(Random.Range(-pop_range_x, +pop_range_x), pop_range_y);
        }
        // Show gain value
        else
        {
            _floatText.color = new Color32(0, 255, 0, 255);
            _rigidbody.velocity = new Vector2(-10, 10); // Toward to the collectables.
        }
        Destroy(gameObject, FloatTime);

    }

    public void SetFloatingValue(int value) 
    {
        // Show damage value
        if (value < 0)
        {
            isPositiveValue = false;
            _floatText.SetText(value.ToString());
        }
        // Show gain value
        else
        {    
            isPositiveValue = true;
            _floatText.SetText("+" + value.ToString());
        }

    }

}