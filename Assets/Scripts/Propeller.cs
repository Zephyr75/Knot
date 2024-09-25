using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : Module
{
    public int power;

    // Start is called before the first frame update
    void Start()
    {
        SetMainInput(KeyCode.T);
        SetSecondInput(KeyCode.T);
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.GetComponent<Editor>().start)
        {
            if (Input.GetKey(GetMainInput()))
            {
                GetComponent<Rigidbody>().AddForce(transform.up * 150 * power);
            }
        }
    }
}
