using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Follow")]
    private Camera cam;
    [Range(0f, 1f)] public float interpolation = 0.1f;
    public Vector3 offset = new Vector3(0f, 2f, -7f);

    private void Start()
    {
        cam = Camera.main;
    }
    public void UpdatePosition(Vector3 targetPosition)
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition + offset, interpolation);
    }
}
