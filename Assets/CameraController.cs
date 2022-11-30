using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    public float zoomInDuration = 2.0f;
    public float transformDuration = 2.0f;
    
    private Camera mainCamera;
    private Vector3 originalPosition;
    private float zoomScale;
    private bool isTransition = false;
    private float elapsed = 0f;
    private Vector3 target;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransition)
        {
            elapsed += Time.unscaledDeltaTime;
            mainCamera.orthographicSize = Mathf.SmoothStep(5.0f, zoomScale, elapsed / zoomInDuration);
            transform.position = new Vector3(
                Mathf.SmoothStep(originalPosition.x, target.x, 2 * elapsed / transformDuration),
                Mathf.SmoothStep(originalPosition.x, target.y, 2 * elapsed / transformDuration),
                originalPosition.z
            );
            if (elapsed > Mathf.Max(zoomInDuration, transformDuration))
            {
                isTransition = false;
                elapsed = 0f;
            }
        }
    }
    
    public void Reset()
    {
        mainCamera.orthographicSize = 5f;
        transform.position = originalPosition;
        elapsed = 0f;
        isTransition = false;
    }
    
    public void StartTransition(float zoomEnd, Vector3 target)
    {
        this.isTransition = true;
        this.zoomScale = zoomEnd;
        this.target = target;
        this.target.z = transform.position.z;
    }
}
