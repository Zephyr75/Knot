using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    private float mouseSensitivityX = 1, mouseSensitivityY = 1, verticalRotation, horizontalRotation;
    public Transform focus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivityX;
        verticalRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalRotation = Mathf.Clamp(verticalRotation, -60, 15);
        focus.localEulerAngles = Vector3.left * verticalRotation + Vector3.up * horizontalRotation;
        transform.localRotation = focus.localRotation;
        transform.position = focus.position - focus.forward * 15;
    }
}