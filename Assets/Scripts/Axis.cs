using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis : Module
{
    public int speed;

    private float verticalRotation, horizontalRotation;

    // Start is called before the first frame update
    void Start()
    {
        SetMainInput(KeyCode.LeftShift);
        SetSecondInput(KeyCode.LeftControl);
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.GetComponent<Editor>().start)
        {
            //Controls for driving
            bool up = Input.GetKey(GetMainInput());
            bool down = Input.GetKey(GetSecondInput());

            if (up)
            {
                verticalRotation -= 1;
            }
            if (down)
            {
                verticalRotation += 1;
            }
        }
        transform.localEulerAngles = Vector3.left * verticalRotation + Vector3.up * horizontalRotation;
    }
}
