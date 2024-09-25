using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    private Rigidbody main;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Rigidbody rb in transform.parent.GetComponentsInChildren<Rigidbody>())
        {
            if (rb.tag == "Main")
            {
                main = rb;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //GetComponent<Rigidbody>().AddForce(main.velocity.magnitude * main.GetComponent<MainBloc>().nbrOfElements * -main.velocity);
        }
    }
}
