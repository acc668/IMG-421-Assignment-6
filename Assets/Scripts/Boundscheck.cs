using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of whether a GameObject is on screen or not.
/// Also provides camWidth and camHeight for other scripts to use.
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = false;

    [Header("Set Dynamically")]
    public bool isOnScreen = false;
    public float camWidth;
    public float camHeight;

    // Off-screen direction flags
    public bool offUp, offDown, offLeft, offRight;

    void Awake()
    {
        // Calculate camera dimensions based on orthographic size or FOV
        Camera cam = Camera.main;
        if (cam.orthographic)
        {
            camHeight = cam.orthographicSize;
            camWidth = camHeight * cam.aspect;
        }
        else
        {
            // Perspective camera - calculate based on distance from camera
            float dist = Mathf.Abs(transform.position.z - cam.transform.position.z);
            camHeight = dist * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            camWidth = camHeight * cam.aspect;
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        // Reset flags
        offUp = offDown = offLeft = offRight = false;

        isOnScreen = true;

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
            isOnScreen = false;
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
            isOnScreen = false;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
            isOnScreen = false;
        }
        if (pos.y < -camHeight - radius)
        {
            // offDown means fully below the screen (extra radius below)
            offDown = true;
            isOnScreen = false;
        }

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offUp = offDown = offLeft = offRight = false;
        }
        else
        {
            transform.position = pos;
        }
    }
}