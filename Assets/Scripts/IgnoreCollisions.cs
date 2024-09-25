using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
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
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name != "Mirror")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), col.gameObject.GetComponent<Collider>(), true);
        }
    }
}
