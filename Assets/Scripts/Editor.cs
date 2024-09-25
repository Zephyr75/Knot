using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Editor : MonoBehaviour
{
    public GameObject machine, one, two, three, four, five, six, seven, eight, nine, zero;
    public LayerMask layerBoth, layerSpawn;
    public float maxDistance;
    public RaycastHit hit, hitRay;
    public Camera cam;
    public Material transparent;
    public bool defMainInput, defSecondInput, start;


    private bool defRot, defWeight;
    private GameObject actualHit, preview, module, actualSpawn, toLink, first, second;
    private Vector3 actualNormal;
    private Quaternion preRot;
    private Vector3 prePos;
    private List<Vector3> around = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        module = one;
        preRot = Quaternion.identity;
        prePos = Vector3.zero;
        around.Add(Vector3.forward);
        around.Add(-Vector3.forward);
        around.Add(Vector3.right);
        around.Add(-Vector3.right);
        around.Add(Vector3.up);
        around.Add(-Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        StartAction();
        if (!start && Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, maxDistance, layerBoth))
        {
            if (actualHit != hit.transform.gameObject || actualNormal != hit.normal || actualSpawn != module)
            {
                Preview();
                actualSpawn = module;
                actualNormal = hit.normal;
            }
            actualHit = hit.transform.gameObject;
            if (!defRot && !defMainInput && !defSecondInput && !defWeight)
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    Build();
                }
                Delete();
                ChooseNext();
            }
            EditInput();
            EditRotation();
            EditWeight();
        }
    }

    private void ChooseNext()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            module = one;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            module = two;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            module = three;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            module = four;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            module = five;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            module = six;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            module = seven;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            module = eight;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            module = nine;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            module = zero;
        }
    }

    private void Preview()
    {
        Destroy(preview);
        prePos = ModulePosition();
        preview = GameObject.Instantiate(module, prePos, preRot);
        foreach (Renderer child in preview.GetComponentsInChildren<Renderer>())
        {
            child.material = transparent;
        }
        foreach (Collider child in preview.GetComponentsInChildren<Collider>())
        {
            child.enabled = false;
        }
    }

    private void Build()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(preview);
            GameObject newMod = GameObject.Instantiate(module, prePos, preRot);
            newMod.transform.parent = machine.transform;

            if (newMod.transform.GetComponent<Module>().isSpring)
            {
                foreach (Collider col in newMod.transform.parent.GetComponentsInChildren<Collider>())
                {
                    Physics.IgnoreCollision(col, hit.collider, true);
                }
            }
            
            foreach (Vector3 direction in around)
            {
                if (Physics.Raycast(prePos + direction * 9 / 20, direction, out hitRay, .15f, layerSpawn))
                {
                    newMod.transform.GetComponent<Module>().Link(hitRay.transform.gameObject);
                }
            }
        }
    }

    private void Delete()
    {
        if (Input.GetMouseButtonDown(1) && hit.collider.tag != "Main")
        {
            if (hit.transform.tag.Contains("Child"))
            {
                Destroy(hit.collider.transform.parent.gameObject);
            }
            else
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void EditRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Rotate");
            defRot = true;
        }
        if (defRot && !Input.GetKey(KeyCode.R))
        {
            bool up = Input.GetKey(KeyCode.UpArrow);
            bool down = Input.GetKey(KeyCode.DownArrow);
            bool right = Input.GetKey(KeyCode.RightArrow);
            bool left = Input.GetKey(KeyCode.LeftArrow);
            if (up)
            {
                preview.transform.Rotate(90, 0, 0);
            }
            if (down)
            {
                preview.transform.Rotate(-90, 0, 0);
            }
            if (right)
            {
                preview.transform.Rotate(0, 90, 0);
            }
            if (left)
            {
                preview.transform.Rotate(0, -90, 0);
            }
            if (up || down || left || right)
            {
                preRot = preview.transform.rotation;
                defRot = false;
            }
        }
    }

    private void EditInput()
    {
        //Main input
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Enter main input");
            defMainInput = true;
        }
        if (defMainInput && !Input.GetKey(KeyCode.I))
        {
            foreach (KeyCode input in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(input))
                {
                    if (hit.transform.tag.Contains("Child"))
                    {
                        foreach (Module mod in hit.transform.parent.GetComponentsInChildren<Module>())
                        {
                            mod.SetMainInput(input);
                        }
                    }
                    else
                    {
                        hit.transform.GetComponent<Module>().SetMainInput(input);
                    }
                    StartCoroutine(WaitForZQSD());
                }
            }
        }
        //Secondary input
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Enter secondary input");
            defSecondInput = true;
        }
        if (defSecondInput && !Input.GetKey(KeyCode.O))
        {
            foreach (KeyCode input in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(input))
                {
                    if (hit.transform.tag.Contains("Child"))
                    {
                        foreach (Module mod in hit.transform.parent.GetComponentsInChildren<Module>())
                        {
                            mod.SetSecondInput(input);
                        }
                    }
                    else
                    {
                        hit.transform.GetComponent<Module>().SetSecondInput(input);
                    }
                    StartCoroutine(WaitForZQSD());
                }
            }
        }
    }

    private IEnumerator WaitForZQSD()
    {
        yield return new WaitForSeconds(.1f);
        defMainInput = false;
        defSecondInput = false;
    }

    private void EditWeight()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Enter weight");
            defWeight = true;
        }
        if (defWeight && !Input.GetKey(KeyCode.W))
        {
            foreach (KeyCode input in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(input) && input.ToString().Contains("Alpha"))
                {
                    if (hit.transform.GetComponent<Rigidbody>() != null)
                    {
                        if (hit.transform.tag.Contains("Child"))
                        {
                            hit.transform.parent.GetComponent<Rigidbody>().mass = input.ToString() == "Alpha0" ? 1 : float.Parse(input.ToString().Substring(5)) / 10;
                            Debug.Log(hit.transform.parent.GetComponent<Rigidbody>().mass);
                        }
                        else
                        {
                            hit.transform.GetComponent<Rigidbody>().mass = input.ToString() == "Alpha0" ? 1 : float.Parse(input.ToString().Substring(5)) / 10;
                            Debug.Log(hit.transform.GetComponent<Rigidbody>().mass);
                        }
                        defWeight = false;
                    }
                }
            }
        }
    }

    private void StartAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (start)
            {
                SceneManager.LoadScene("Class");
            }
            else
            {
                Destroy(preview);
                foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
                {
                    rb.useGravity = true;
                }
                foreach (Collider col in GetComponentsInChildren<Collider>())
                {
                    if (col.tag == "ChildEditor")
                    {
                        Destroy(col.gameObject);
                    }
                    else
                    {
                        col.enabled = true;
                    }
                }
            }
            start = !start;
        }
    }

    private Vector3 ModulePosition()
    {
        float x = Mathf.Round(hit.collider.transform.position.x) + Mathf.Round(hit.normal.x);
        float y = Mathf.Round(hit.collider.transform.position.y) + Mathf.Round(hit.normal.y);
        float z = Mathf.Round(hit.collider.transform.position.z) + Mathf.Round(hit.normal.z);
        return new Vector3(x, y, z);
    }
}
