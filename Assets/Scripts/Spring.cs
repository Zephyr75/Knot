using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : Module
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.GetComponent<Editor>().start)
        {
            transform.rotation = transform.parent.rotation;
            if (transform.localPosition.y < .5f && transform.localPosition.y > -.5f)
            {
                transform.localScale = new Vector3(50, 100 * (0.5f + transform.localPosition.y), 50);
            }
        }
        //Debug.Log(transform.localPosition.y);
    }
}
