using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    public Transform coming;
    private GameObject leaving;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ray")
        {
            leaving = GameObject.Instantiate(coming.gameObject, col.GetContact(0).point, transform.rotation);
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name == "Ray")
        {
            Debug.Log(col.GetContact(0).point);
            leaving.transform.position = col.GetContact(0).point;
            leaving.transform.up = Vector3.Reflect(-coming.up, transform.up);
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Ray")
        {
            Destroy(leaving);
        }
    }
}
