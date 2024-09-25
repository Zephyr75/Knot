using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public bool isSpring, isAxis;
    public GameObject sphere, machine;
    
    private KeyCode mainInput = KeyCode.UpArrow, secondInput = KeyCode.DownArrow, thirdInput = KeyCode.LeftArrow, fourthInput = KeyCode.RightArrow;
    private bool isFixed;
    private Transform focus = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed)
        {
            transform.localRotation = focus.transform.localRotation;
        }

        switch (secondInput.ToString())
        {
            case "S":
                thirdInput = KeyCode.Q;
                fourthInput = KeyCode.D;
                break;
            case "DownArrow":
                thirdInput = KeyCode.LeftArrow;
                fourthInput = KeyCode.RightArrow;
                break;
            default:
                thirdInput = mainInput;
                fourthInput = mainInput;
                break;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(mainInput.ToString() + " " + secondInput.ToString() + " " + thirdInput.ToString() + " " + fourthInput.ToString());
        }
    }

    public void Link(GameObject otherObject)
    {
        Vector3 thisPos = gameObject.transform.position, otherPos = otherObject.transform.position;
        GameObject newSphere = GameObject.Instantiate(sphere, new Vector3((thisPos.x + otherPos.x) / 2, (thisPos.y + otherPos.y) / 2, (thisPos.z + otherPos.z) / 2), Quaternion.identity);
        newSphere.GetComponent<Sphere>().DefLink(gameObject, otherObject);
        newSphere.transform.parent = otherObject.transform;
        
        if (otherObject.GetComponent<Module>().isAxis)
        {
            transform.parent = otherObject.transform;
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            isFixed = true;
            foreach (Transform child in otherObject.GetComponentInChildren<Transform>())
            {
                if (child.name == "Focus")
                {
                    focus = child;
                }
            }
        }
        else
        {
            FixedJoint firstJoint = gameObject.AddComponent<FixedJoint>();
            firstJoint.connectedBody = otherObject.GetComponent<Rigidbody>();
            firstJoint.breakForce = 10000;
        }
    }

    public void SetMainInput(KeyCode input)
    {
        mainInput = input;
    }

    public KeyCode GetMainInput()
    {
        return mainInput;
    }

    public void SetSecondInput(KeyCode input)
    {
        secondInput = input;
    }

    public KeyCode GetSecondInput()
    {
        return secondInput;
    }

    public KeyCode GetThirdInput()
    {
        return thirdInput;
    }

    public KeyCode GetFourthInput()
    {
        return fourthInput;
    }
}
