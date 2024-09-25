using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pale : Module
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetMainInput(transform.parent.GetComponent<Module>().GetMainInput());
        if (machine.GetComponent<Editor>().start)
        {
            if (Input.GetKey(GetMainInput()))
            {
                transform.Rotate(Vector3.up * 10);
            }
        }
    }
}
