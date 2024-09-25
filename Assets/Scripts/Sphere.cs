using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private GameObject first, second;
    private bool defined;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (defined)
        {
            if (first == null || second == null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void DefLink(GameObject first, GameObject second)
    {
        this.first = first;
        this.second = second;
        defined = true;
    }
}
