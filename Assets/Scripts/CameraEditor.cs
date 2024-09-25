using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEditor : MonoBehaviour
{
    public GameObject machine;

    private float offsetX, offsetY, verticalRotation, horizontalRotation, movementSpeed = 20;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!machine.GetComponent<Editor>().defMainInput && !machine.GetComponent<Editor>().defSecondInput)
        {
            if (Input.GetKey(KeyCode.Z) || Input.mouseScrollDelta.y == 1)
            {
                transform.position += transform.forward * Time.deltaTime * movementSpeed;
            }
            if (Input.GetKey(KeyCode.S) || Input.mouseScrollDelta.y == -1)
            {
                transform.position -= transform.forward * Time.deltaTime * movementSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * Time.deltaTime * movementSpeed;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.position -= transform.right * Time.deltaTime * movementSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += transform.up * Time.deltaTime * movementSpeed;
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.position -= transform.up * Time.deltaTime * movementSpeed;
            }
        }

        horizontalRotation += Input.GetAxis("Mouse X");
        verticalRotation += Input.GetAxis("Mouse Y");
        //verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        transform.localEulerAngles = Vector3.left * (verticalRotation + offsetY) + Vector3.up * (horizontalRotation + offsetX) + rotation;
    }

    public void ResetCam(Vector3 rotation)
    {
        offsetX = -horizontalRotation;
        offsetY = -verticalRotation;
        this.rotation = rotation;
    }
}