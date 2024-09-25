using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camPlayer, camEditor, camFront;
    private List<Vector3> positions = new List<Vector3>();
    private List<Vector3> rotations = new List<Vector3>();
    private Dictionary<int, int[]> indexes = new Dictionary<int, int[]>();
    private int view, index;
    private bool resetCam;

    // Start is called before the first frame update
    void Start()
    {
        camEditor.SetActive(true);
        camFront.SetActive(true);
        camPlayer.SetActive(false);
        //Front ========== 0
        positions.Add(new Vector3(0, 0, -10));
        rotations.Add(new Vector3(0, 0, 0));
        //Back ========== 1
        positions.Add(new Vector3(0, 0, 10));
        rotations.Add(new Vector3(0, 180, 0));
        //Up ========== 2
        positions.Add(new Vector3(0, 10, 0));
        rotations.Add(new Vector3(90, 0, 0));
        //Down ========== 3
        positions.Add(new Vector3(0, -10, 0));
        rotations.Add(new Vector3(-90, 0, 0));
        //Right ========== 4
        positions.Add(new Vector3(10, 0, 0));
        rotations.Add(new Vector3(0, -90, 0));
        //Left ========== 5
        positions.Add(new Vector3(-10, 0, 0));
        rotations.Add(new Vector3(0, 90, 0));
        //Indexes ========== 0, 1, 2, 3, 4, 5
        indexes.Add(5, new int[] {0, 0, 0, 0, 0, 0});
        indexes.Add(6, new int[] {4, 5, 4, 4, 1, 0});
        indexes.Add(4, new int[] {5, 4, 5, 5, 0, 1});
        indexes.Add(8, new int[] {2, 2, 1, 0, 2, 2});
        indexes.Add(2, new int[] {3, 3, 0, 1, 3, 3});

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            camEditor.SetActive(true);
            camFront.SetActive(true);
            camPlayer.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            camPlayer.SetActive(true);
            camEditor.SetActive(false);
            camFront.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            resetCam = true;
            index = 5;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            resetCam = true;
            index = 6;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            resetCam = true;
            index = 4;
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            resetCam = true;
            index = 8;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            resetCam = true;
            index = 2;
        }
        if (resetCam)
        {
            if (!positions.Contains(camEditor.transform.position))
            {
                view = indexes[index][0];
            }
            else
            {
                switch (view)
                {
                    case 0: view = indexes[index][0]; break;
                    case 1: view = indexes[index][1]; break;
                    case 2: view = indexes[index][2]; break;
                    case 3: view = indexes[index][3]; break;
                    case 4: view = indexes[index][4]; break;
                    default: view = indexes[index][5]; break;
                }
            }
            camEditor.transform.position = positions[view];
            camFront.transform.position = positions[view];
            camEditor.GetComponent<CameraEditor>().ResetCam(rotations[view]);
            camFront.GetComponent<CameraEditor>().ResetCam(rotations[view]);
            resetCam = false;
        }
    }
}
