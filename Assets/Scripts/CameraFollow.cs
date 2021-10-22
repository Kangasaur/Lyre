using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public BoxCollider2D worldBounds;

    float xMin, xMax, yMin, yMax;
    float camY, camX, camSize, camRatio;

    Camera mainCam;

    Vector3 smoothPos;

    public float smoothRate;
    
    void Start()
    {
        mainCam = GetComponent<Camera>();
        xMin = worldBounds.bounds.min.x;
        xMax = worldBounds.bounds.max.x;
        yMin = worldBounds.bounds.min.y;
        yMax = worldBounds.bounds.max.y;

        camSize = mainCam.orthographicSize;
        camRatio = camSize * 16 / 10;
    }
    
    void FixedUpdate()
    {
        camY = Mathf.Clamp(followTransform.position.y, yMin + camSize, yMax - camSize);
        camX = Mathf.Clamp(followTransform.position.x, xMin + camRatio, xMax - camRatio);

        smoothPos = Vector3.Lerp(transform.position, new Vector3(camX, camY, -1000f), smoothRate);
        gameObject.transform.position = smoothPos;
    }
}
